using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs.HitRunStates
{
    public class CombatHitState : HSMState
    {
        public CombatHitState(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, null)
        {
        }
    }
}