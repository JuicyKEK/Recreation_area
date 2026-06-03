using System;
using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;
using Game.Scripts.NPC.HSMBase.ActionWithExpectation;
using Game.Scripts.NPC.NPC.External.NPCActions.Controllers;
using Game.Scripts.NPC.NPC.External.NPCActions.SOActions.Intefaces;
using UnityEngine;

namespace Game.Scripts.NPC.NPC.External.NPCActions.SOActions
{
    [CreateAssetMenu(fileName = "ActionNPCWithTimer", menuName = "AI/New ActionNPC/New ActionNPCWithTimer", order = 51)]
    public class ActionNPCWithTimerSO : ScriptableObject, IActionNPCSO
    {
        [SerializeField] private string _beforeInteractAnimationName;
        [SerializeField] private string _interactAnimationName;
        [SerializeField] private float _timeToInteract;
        [SerializeField] private InteractableNPC _interactableNPC;
        [SerializeField] private InteractionTypes _interactionType;

        public HSMState CreateAction(HSMStateMachine machine, NPCContext context, HSMState parent = null)
        {
            return new ActionWithTimerRoot(machine, context, parent);
        }
    }
}