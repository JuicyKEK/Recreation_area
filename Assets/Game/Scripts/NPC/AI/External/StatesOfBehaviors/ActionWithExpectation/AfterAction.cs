using Game.Scripts.NPC.AI.External.NPC;

namespace Game.Scripts.NPC.HSMBase.ActionWithExpectation
{
    public class AfterAction : HSMState
    {
        public AfterAction(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, null)
        {
        }
    }
}