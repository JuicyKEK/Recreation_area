using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase.SequenceraseBase;

namespace Game.Scripts.NPC.HSMBase.ActionWithExpectation
{
    public class ActionWithTimer : HSMState
    {
        private readonly NPCContext _context;
        
        public ActionWithTimer(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, null)
        {
            _context = context;
            Add(new DelayActivationActivity {seconds = 1});
        }
    }
}