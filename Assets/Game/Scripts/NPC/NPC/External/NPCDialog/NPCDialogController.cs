using System.Collections.Generic;
using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.Player.External.DialogBox.Interfaces;
using UnityEngine;

namespace Game.Scripts.Player.External.DialogBox
{
    public class NPCDialogController : IDialogController //кто будет создавать диалоги? Надо ж в него экшен передавать
    {
        private string _npcName = "NPC"; 
        private List<DialogOption> _dialogOptions = new List<DialogOption>();
        private NPCContext _context;

        public NPCDialogController(NPCContext context)
        {
            _context = context;
            SetDefaultDialog();
        }
        
        /// <summary>
        /// Добавить вариант диалога
        /// </summary>
        public void AddDialogOption(DialogOption option)
        {
            _dialogOptions.Add(option);
        }
        
        /// <summary>
        /// Добавить вариант диалога
        /// </summary>
        public void RemoveDialogOption(DialogOption option)
        {
            _dialogOptions.Remove(option);
        }

        /// <summary>
        /// Очистить все варианты диалога
        /// </summary>
        public void ClearDialogOptions()
        {
            _dialogOptions.Clear();
        }

        public IReadOnlyList<DialogOption> GetDialogOptions()
        {
            return _dialogOptions;
        }

        public void OnDialogStarted()
        {
            _context.IsDialog = true;
            //StartDialog(_dialogOptions);
        }

        public void OnDialogEnded()
        {
            _context.IsDialog = false;
        }

        private void SetDefaultDialog()
        {
            AddDialogOption(new DialogOption("Close", OnDialogEnded));
        }
    }
}

