using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.AI.External.Services;
using Game.Scripts.NPC.AI.External.Services.Interfaces;
using Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs;
using Game.Scripts.NPC.NPC.External.NPCActions.SOActions.Intefaces;

namespace Game.Scripts.NPC.HSMBase
{
    public class HsmNPC : IHsmNPC
    {
        private HSMState _currentState;
        private BaseNPCRoot _rootState;
        private ICreateHSMStateService _stateService;
        private HSMStateMachineBuilder _machineBuilder; 
        private HSMStateMachine _machine;
        private NPCContext _context;
        
        public HsmNPC(IActionNPCSO newActionState, NPCContext context)
        {
            _context = context;
            
            InitializeStateMachine();
            ReplaceActionState(newActionState);
        }

        public void Tick(float deltaTime)
        {
            _machine.Tick(deltaTime); //делать ли проверку на пустоту?
        }
        
        public void ReplaceActionState(IActionNPCSO newActionState)
        {
            if (_stateService == null)
            {
                _stateService = new CreateHSMStateService();
            }
            
            _currentState = _stateService.CreateHSMState(newActionState);
            _rootState.SetActionState(_currentState);
        }
        
        
        private void InitializeStateMachine() //Создать базовый рут в котором можно будет поменять экн рут
        {
            if (_rootState == null)
            {
                _rootState = new BaseNPCRoot(null, _context);
            }
            
            if (_machineBuilder == null)
            {
                _machineBuilder = new HSMStateMachineBuilder(_rootState);
                _machine = _machineBuilder.Build();
            }
        }
    }
}