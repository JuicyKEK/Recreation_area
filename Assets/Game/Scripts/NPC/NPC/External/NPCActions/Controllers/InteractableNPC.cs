using System;
using Game.Scripts.NPC.NPC.External.Conntrollers.Intrfaces;
using UnityEngine;

namespace Game.Scripts.NPC.NPC.External.NPCActions.Controllers
{
    public abstract class InteractableNPC : MonoBehaviour, IInteractableNPC
    {
        public abstract void Interact();
    }
}