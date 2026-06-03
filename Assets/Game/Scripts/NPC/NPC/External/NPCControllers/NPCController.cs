using System;
using Game.Scripts.Inventory;
using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.AI.External.Services;
using Game.Scripts.NPC.AI.External.Services.Interfaces;
using Game.Scripts.NPC.HSMBase;
using Game.Scripts.NPC.RunTime.NPC;
using Game.Scripts.NPC.RunTime.NPCPersonalityCreator;
using Game.Scripts.Player.External.DialogBox;
using Game.Scripts.Player.External.DialogBox.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.NPC.NPC.External.Conntrollers
{
    
    public class NPCController : MonoBehaviour, IInteraction
    {
        [Header("Personality")]
        [SerializeField] private NPCPersonalitySO _fixedPersonalitySO;
        [Header("AIComponents")]
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        private NPCPersonality _currentPersonality;
        private NPCContext _npcContext;
        private ServiceNPCPersonalityCreator _personalityCreator;
        private IHsmNPC _hsmNPC;
        private IDialogController _dialogController;
        
        private INPCContextCreatorServices _npcContextCreator;
        
        public void Awake() //Потом удалить
        {
            //Init();
        }
        
        public void Init()
        {
            InitServices();
            InitNPCCharacter();
            InitDialogs();
            InitHSMNPC();
        }

        public void Interact()
        {
            _dialogController.OnDialogStarted();
        }

        private void InitServices()
        {
            if (_personalityCreator == null)
            {
                _personalityCreator = new ServiceNPCPersonalityCreator();
            }
            
            if (_npcContextCreator == null)
            {
                _npcContextCreator = new NPCContextCreatorServices();
            }
        }

        private void InitNPCCharacter()
        {
            _currentPersonality = _fixedPersonalitySO != null ?
                _personalityCreator.CreateNPCPersonality(_fixedPersonalitySO)
                : null; //Интегрируем фиксированную персональ
            
            if (_npcContext == null)
            {
                //_npcContext = _npcContextCreator.NPCContextCreat(_currentPersonality, _navMeshAgent);
                _npcContext = new NPCContext(_navMeshAgent, _currentPersonality, _dialogController); //угар 1
            }

            _npcContext.IsIdle = true;
        }
        
        private void InitDialogs()
        {
            if (_dialogController == null)
            {
                _dialogController = new NPCDialogController(_npcContext); //угар 2
            }
        }

        private void InitHSMNPC()
        {
            if (_hsmNPC == null)
            {
                _hsmNPC = new HsmNPC(null, _npcContext);
            }
        }

        private void Update()
        {
            _hsmNPC.Tick(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FirstPersonController player))
            {
                _npcContext.TargetObject = player.gameObject.transform;
            }
        }
    }
}