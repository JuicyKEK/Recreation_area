using System;
using Game.Scripts.NPC.RunTime.NPC;
using Game.Scripts.Player.External.DialogBox.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.NPC.AI.External.NPC
{
    [Serializable]
    public class NPCContext //Надо закрыть его будет
    {
        public Vector3 Move; //?
        public Vector3 Velocity; //?
        public bool IsAlive;
        public bool InDanger;
        public bool IsDialog;
        public bool IsFollowing;
        public bool IsIdle;
        public bool IsSleeping;
        public bool IsDeepSleeping;
        public bool InProgressQueue;
        public bool InProgress;
        public float PercentageProgress;
        public float HPLevel;
        public float LevelTrustPlayer;
        public Animator Anim;
        public Rigidbody Rb;
        public Transform TargetObject;

        public NPCPersonality Personality => _personality;
        public NavMeshAgent Agent => _navMeshAgent;
        public IDialogController Dialog => _dialogController;

        private NPCPersonality _personality;
        private NavMeshAgent _navMeshAgent;
        private IDialogController _dialogController;
        
        public NPCContext(NavMeshAgent agent, NPCPersonality personality, IDialogController dialogController)
        {
            _navMeshAgent = agent;
            _personality = personality;
            _dialogController = dialogController;
            
            HPLevel = personality.MaxHP;
            LevelTrustPlayer = personality.StartLevelOfTrust;
        }
    }
}