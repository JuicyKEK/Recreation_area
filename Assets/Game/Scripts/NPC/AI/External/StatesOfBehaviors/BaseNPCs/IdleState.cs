using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;
using UnityEngine;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs
{
    public class IdleState : HSMState
    {
        private NPCContext _context;
        
        public IdleState(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, parent)
        {
            _context = context;
        }
        
        protected override void OnEnter()
        {
            _context.Agent.isStopped = true;
            Debug.Log("Entered IdleState");
            //TODO: Запуск анимации idle
            //TODO: Запихать сюда выбор дальнейших действий
        }
    }
}