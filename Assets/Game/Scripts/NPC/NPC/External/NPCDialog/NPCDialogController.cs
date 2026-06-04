using System;
using System.Collections.Generic;
using Game.Scripts.NPC.AI.External.NPC;
using Game.Scripts.Player.External.DialogBox.Interfaces;
using UnityEngine;

namespace Game.Scripts.Player.External.DialogBox
{
    public class NPCDialogController : IDialogController 
    {
        private string _npcName = "NPC"; 
        private List<DialogOption> _dialogOptions = new List<DialogOption>();
        private bool _isDefaultDialogCreated = false;
        
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
            _isDefaultDialogCreated = false;
            _dialogOptions.Clear();
        }

        public IReadOnlyList<DialogOption> GetDialogOptions()
        {
            Debug.Log("__________________");
            Debug.Log(_dialogOptions.Count);
            return _dialogOptions;
        }

        public void OnDialogStarted(Action callback)
        {
            //_context.IsDialog = true;
            //StartDialog(_dialogOptions);
            if (!_isDefaultDialogCreated)
            {
                SetDefaultDialog(callback);
            }
        }

        public void OnDialogEnded()
        {
            //_context.IsDialog = false;
        }

        private void SetDefaultDialog(Action callback)
        {
            Debug.Log("________SetDefaultDialog__________");
            _isDefaultDialogCreated = true;
            
            AddDialogOption(new DialogOption("Close", () =>
            {
                OnDialogEnded();
                callback?.Invoke();
            }));
        }
    }
}

