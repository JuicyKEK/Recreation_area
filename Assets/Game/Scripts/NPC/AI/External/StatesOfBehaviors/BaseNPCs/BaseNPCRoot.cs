using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;
using JuicyDI;
using UnityEngine;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs
{
    public class BaseNPCRoot : HSMState
    {
        public readonly HSMState IdleBaseState;
        public readonly HSMState FollowingBaseState;
        public readonly HSMState SleepingBaseState;
        public readonly HSMState CombatBaseState;
        public readonly HSMState DialogBaseState;
        
        public HSMState ActionBaseState;
        public HSMState CurrentState;
        
        private NPCContext _context;
        
        public BaseNPCRoot(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, parent)
        {
            _context = context;
            
            // Инициализация базовых состояний (заглушки, нужно будет создать реальные классы)
            IdleBaseState = new IdleState(machine, _context, this);
            FollowingBaseState = new FollowingState(machine, _context, this);
            SleepingBaseState = new SleepingState(machine, _context, this);
            CombatBaseState = new HitRunStateRoot(machine, _context, this);
            DialogBaseState = BinController.GetContext().ConstructorLateInjection<DialogState>(machine, _context, this);
            //DialogBaseState = new DialogState(machine, _context, this);
        }

        /// <summary>
        /// Метод для замены ActionState из внешних скриптов
        /// Например: ActionNPCWithTimerSO.CreateAction может создать ActionWithTimerRoot и передать сюда
        /// </summary>
        public void SetActionState(HSMState newActionState)
        {
            ActionBaseState = newActionState;
        }

        protected override HSMState GetInitialState() => IdleBaseState;
        
        protected override HSMState GetTransition()
        {
            if (_context.IsDialog && DialogBaseState != CurrentState)
            {
                CurrentState = DialogBaseState;
                return DialogBaseState;
            }
            
            if (_context.IsIdle && IdleBaseState != CurrentState)
            {
                CurrentState = IdleBaseState;
                return IdleBaseState;
            }
            
            if (_context.IsFollowing && FollowingBaseState != CurrentState)
            {
                CurrentState = FollowingBaseState;
                return FollowingBaseState;
            }
            
            if (_context.InDanger)
            {
                return CombatBaseState;
            }

            if (_context.IsSleeping)
            {
                return SleepingBaseState;
            }

            if (ActionBaseState != null && false)
            {
                return ActionBaseState;
            }
            
            return null;
        }
    }
}