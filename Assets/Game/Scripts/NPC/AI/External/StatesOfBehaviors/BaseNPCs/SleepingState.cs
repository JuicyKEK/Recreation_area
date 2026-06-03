using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs
{
    public class SleepingState : HSMState
    {
        private NPCContext _context;
        
        public SleepingState(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, parent)
        {
            _context = context;
        }
        
        protected override void OnEnter()
        {
            // Логика сна
        }
    }
}