using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.NPC.HSMBase;
using Game.Scripts.Player.External.DialogBox.Interfaces;
using JuicyDI.Attributes;
using UnityEngine;

namespace Game.Scripts.NPC.AI.External.StatesOfBehaviors.BaseNPCs
{
    public class DialogState : HSMState
    {
        [Inject] public IDialogBox _dialogBox;
        
        private NPCContext _context;
        
        public DialogState(HSMStateMachine machine, NPCContext context, HSMState parent = null) : base(machine, parent)
        {
            _context = context;
            Debug.Log(_context != null);
            Debug.Log(_dialogBox != null);
        }
        
        protected override void OnEnter()
        {
            Debug.Log("Entered DialogState");
            _context.IsIdle = false;
            Debug.Log(_context != null);
            Debug.Log(_dialogBox != null);
            Debug.Log(_context.Dialog != null);
            _dialogBox.StartDialog(_context.Dialog.GetDialogOptions());
        }
    }
}