using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;

namespace Game.Scripts.NPC.NPC.External.NPCActions.SOActions.Intefaces
{
    public interface IActionNPCSO
    {
        public HSMState CreateAction(HSMStateMachine machine, NPCContext context, HSMState parent = null);
    }
}