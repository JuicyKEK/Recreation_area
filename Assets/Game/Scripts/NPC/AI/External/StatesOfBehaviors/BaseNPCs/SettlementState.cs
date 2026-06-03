using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs
{
    public class SettlementState : HSMState
    {
        private NPCContext _context;
        
        public SettlementState(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, parent)
        {
            _context = context;
        }
        
        protected override void OnEnter()
        {
            // Логика нахождения в поселении
        }
    }
}