using Game.Scripts.NPC.AI.External.Services.Interfaces;
using Game.Scripts.NPC.HSMBase;
using Game.Scripts.NPC.NPC.External.NPCActions.SOActions.Intefaces;

namespace Game.Scripts.NPC.AI.External.Services
{
    public class CreateHSMStateService : ICreateHSMStateService
    {
        public HSMState CreateHSMState(IActionNPCSO actionNPCSO)
        {
            return null; //TODO: Доделать конвертер СО в состояние
        }
    }
}