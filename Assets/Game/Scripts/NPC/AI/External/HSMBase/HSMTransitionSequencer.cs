using System;
using System.Collections.Generic;
using System.Threading;
using Game.Scripts.NPC.HSMBase.Interfaces;
using Game.Scripts.NPC.HSMBase.SequenceraseBase;
using UnityEngine;

namespace Game.Scripts.NPC.HSMBase
{
    public class HSMTransitionSequencer
    {
        public readonly HSMStateMachine Machine;
        
        private ISequencer _sequencer; //Текущая фаза
        private Action _nextPhase; //Делегат, что запускается после завершения текущей фазы
        private (HSMState from, HSMState to) _pending; //Очередь для запуска следующего перехода
        private HSMState _lastForm, _lastTo;
        
        private CancellationTokenSource cts;
        bool UseSequential = false;     
        
        public HSMTransitionSequencer(HSMStateMachine stateMachine)
        {
            Machine = stateMachine;
        }

        static List<HSMState> StatesToExit(HSMState from, HSMState lca)
        {
            var list = new List<HSMState>();
            for (var s = from; s != null && s != lca; s = s.Parent)
            {
                list.Add(s);
            }
            
            return list;
        }
        
        static List<HSMState> StatesToEnter(HSMState to, HSMState lca)
        {
            var stack = new Stack<HSMState>();
            for (var s = to; s != lca; s = s.Parent)
            {
                stack.Push(s);
            }
            
            return new List<HSMState>(stack);
        }
        
        static List<PhaseStep> GatherPhaseSteps(List<HSMState> chain, bool deactivate) {
            var steps = new List<PhaseStep>();

            for (int i = 0; i < chain.Count; i++) {
                var st = chain[i];
                var acts = chain[i].Activities;
                for (int j = 0; j < acts.Count; j++){
                    var a = acts[j];
                    bool include = deactivate ? (a.Mode == ActivityMode.Active)
                        : (a.Mode == ActivityMode.Inactive);
                    if (!include) continue;

                    Debug.Log($"[Phase {(deactivate?"Exit":"Enter")}] state={st.GetType().Name}, activity={a.GetType().Name}, mode={a.Mode}");

                    steps.Add(ct => deactivate ? a.DeactivateAsync(ct) : a.ActivateAsync(ct));
                }
            }
            return steps;
        }
        
        //Запрос на переход от одного состояния к другому
        public void RequestTransition(HSMState from, HSMState to)
        {
            if (to == null || from == to)
            {
                return;
            }

            if (_sequencer != null)
            {
                _pending = (from, to);
                return;
            }
            
            BeginTransition(from, to);
        }

        private void BeginTransition(HSMState from, HSMState to)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            
            var lca = Lca(from, to);
            var exitChain = StatesToExit(from, lca);
            var enterChain = StatesToEnter(to, lca);
            //Деактивируем старые ветки
            var exitSteps = GatherPhaseSteps(exitChain, false);
            _sequencer = UseSequential 
                ? new SequentialPhase(exitSteps, cts.Token)
                : new ParallelPhase(exitSteps, cts.Token);
            _sequencer.Start();

            _nextPhase = () =>
            {
                Machine.ChangeState(from, to);
                var enterSteps = GatherPhaseSteps(enterChain, false);
                _sequencer = UseSequential 
                    ? new SequentialPhase(exitSteps, cts.Token)
                    : new ParallelPhase(exitSteps, cts.Token);
                _sequencer.Start();
            };
        }

        private void EndTransition()
        {
            _sequencer = null;

            if (_pending.to != null)
            {
                var p = _pending;
                _pending.to = null;
                _pending.from = null;
                BeginTransition(p.from, p.to);
            }
        }

        public void Tick(float deltaTime)
        {
            if (_sequencer != null)
            {
                if (_sequencer.Update())
                {
                    if (_nextPhase != null)
                    {
                        var n = _nextPhase;
                        _nextPhase = null;
                        n();
                    } else
                    {
                        EndTransition();
                    }
                }
                return;
            }
            
            Machine.InternalTick(deltaTime);
        }
        
        //Вычислите наименьшего общего предка двух состояний.
        public static HSMState Lca(HSMState a, HSMState b)
        {
            var ap = new HashSet<HSMState>();
            for (var s = a; s != null; s = s.Parent)
            {
                ap.Add(s);
            }
            
            for (var s = b; s != null; s = s.Parent)
            {
                if(ap.Contains(s))
                {
                    return s;
                }
            }
            
            return null;
        }
    }
}