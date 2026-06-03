using UnityEngine;
using System.Collections.Generic;
using Game.Scripts.NPC.HSMBase.Interfaces;

namespace Game.Scripts.NPC.HSMBase
{
    public abstract class HSMState
    {
        public readonly HSMStateMachine Machine;
        public readonly HSMState Parent;
        public HSMState ActiveChild;

        public IReadOnlyList<IActivity> Activities => _activities;
        
        private readonly List<IActivity> _activities = new List<IActivity>(); 
       

        public HSMState(HSMStateMachine machine, HSMState parent = null)
        {
            Machine = machine;
            Parent = parent;
        }

        public void Add(IActivity a)
        {
            if (a != null)
            {
                _activities.Add(a);
            }
        }
        
        protected virtual HSMState GetInitialState() => null; //Состояние при ините (null - значит что узел конечный)
        protected virtual HSMState GetTransition() => null; //Хочу ли переключится в этом кадре (null - если нет)

        protected virtual void OnEnter() {}
        protected virtual void OnExit() {}
        protected virtual void OnUpdate(float deltaTime) {}

        internal virtual void Enter()
        {
            if (Parent != null)
            {
                Parent.ActiveChild = this;
            }

            OnEnter();
            HSMState init = GetInitialState();
            if (init != null)
            {
                init.OnEnter();
            }
        }

        internal virtual void Exit()
        {
            if (ActiveChild != null)
            {
                ActiveChild.OnExit();
            }
            
            ActiveChild = null;
            OnExit();
        }

        internal virtual void Update(float deltaTime) //Срашна, мб переделать?
        {
            HSMState t = GetTransition();
            if (t != null)
            {
                Machine.Sequencer.RequestTransition(this, t);
                return;
            }

            if (ActiveChild != null)
            {
                ActiveChild.Update(deltaTime);
            }
            
            OnUpdate(deltaTime);
        }
        
        //Возвращает самое глубокое текущее активное состояние потомка (конечную точку активного пути).
        public HSMState Leaf()
        {
            HSMState s = this;
            while (s.ActiveChild != null)
            {
                s = s.ActiveChild;
            }
            
            return s;
        }
        
        //Возвращает это состояние, а затем каждого предка вплоть до корня (self -> parent -> ... -> root).
        public IEnumerable<HSMState> PathToRoot()
        {
            for (HSMState s = this; s != null; s = s.Parent)
            {
                yield return s;
            }
        }
    }
}