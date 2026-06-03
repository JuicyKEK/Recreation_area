using System;
using Game.Scripts.Extensions.StringDropdownDrawer;
using UnityEngine;

namespace Game.Scripts.NPC.NPC.External.NPCActions
{
    [Serializable]
    public class InteractionTypes
    {
        public string CurrentInteract => currentInteract;
        
        [StringDropdown("interactions")]
        [SerializeField] private string currentInteract;
        [HideInInspector]
        [SerializeField] private string[] interactions = { "Cooking", "Swimming", "Walking", "Guitar" };
    }
}