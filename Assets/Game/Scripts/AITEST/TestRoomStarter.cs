using System.Collections.Generic;
using Game.Scripts.NPC.NPC.External.Conntrollers;
using JuicyDI;
using JuicyDI.Attributes;
using UnityEngine;

namespace Game.Scripts.Game.GameStarter
{
    [JDIMonoController]
    public class TestRoomStarter : MonoBehaviour
    {
        [Inject] private List<ISequence> m_StartSequence;
        [Inject] private List<IUpdateSequence> m_UpdateSequence;

        [SerializeField] private MainJDIController m_MainJDIController;
        [SerializeField] private NPCController m_NPCController; //только для теста механик

        //private GameSequenceController m_GameSequenceController;
        private IGameUpdateSequenceController m_GameUpdateSequenceController;
        
        private void Start()
        {
            m_MainJDIController.Init();
            _ = new GameSequenceController(m_StartSequence);
            m_GameUpdateSequenceController = new GameUpdateSequenceController(m_UpdateSequence);
            
            m_NPCController.Init();
        }

        private void Update()
        {
            m_GameUpdateSequenceController.UpdateSequence();
        }
    }
}