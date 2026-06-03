using Game.Scripts.NPC.NPC.External.NPCActions.SOActions.Intefaces;

namespace Game.Scripts.NPC.HSMBase
{
    public interface IHsmNPC
    {
        void Tick(float deltaTime);
        void ReplaceActionState(IActionNPCSO newActionState);
    }
}