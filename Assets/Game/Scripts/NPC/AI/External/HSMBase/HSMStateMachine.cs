using System.Collections.Generic;

namespace Game.Scripts.NPC.HSMBase
{
    public class HSMStateMachine
    {
        public readonly HSMState Root;
        public readonly HSMTransitionSequencer Sequencer;

        private bool _started;

        public HSMStateMachine(HSMState root)
        {
            Root = root;
            Sequencer = new HSMTransitionSequencer(this);
        }

        public void Start()
        {
            if (_started)
            {
                return;
            }
            
            _started = true;
            Root.Enter();
        }

        public void Tick(float deltaTime)
        {
            if (!_started)
            {
                Start();
            }
            
            Sequencer.Tick(deltaTime);
        }
        
        internal void InternalTick(float deltaTime) => Root.Update(deltaTime);

        public void ChangeState(HSMState from, HSMState to)
        {
            if (from == to || from == null || to == null)
            {
                return;
            }
            
            HSMState lca = HSMTransitionSequencer.Lca(from, to);

            for (HSMState s = from; s != lca; s = s.Parent)
            {
                s.Exit();
            }
            
            var stack = new Stack<HSMState>();
            for (HSMState s = to; s != lca; s = s.Parent)
            {
                stack.Push(s);
            }

            while (stack.Count > 0)
            {
                stack.Pop().Enter();
            }
        }
    }
}