using Game.Scripts.NPC.HSMBase;
using Game.Scripts.NPC.NPC.External.NPCActions.SOActions.Intefaces;

namespace Game.Scripts.NPC.AI.External.Services.Interfaces
{
    public interface ICreateHSMStateService
    {
        public HSMState CreateHSMState(IActionNPCSO actionNPCSO);
    }
}