using System;
using System.Collections.Generic;
using Game.Scripts.Player.External.DialogBox.Interfaces;
using JuicyDI;
using JuicyDI.Attributes;
using UnityEngine;

namespace Game.Scripts.Player.External.DialogBox
{
  
    [JDIMonoController]
    [SequenceParticipant(111)]
    public class DialogBoxController : MonoBehaviour, IDialogBox, ISequence
    {
        public bool IsVisible => _view != null && _view.IsVisible; //мб и не надо
        
        [SerializeField] private DialogBoxView _view;
        
        private IDialogController _currentDialogController;
        private DialogOption _exitDialogOption = new DialogOption("Close", null); //вызвать изменение
                                                                    //состояния с возвращением к тому чем занимался

        public void MethodInit()
        {
            _view.Init();
        }

        public void MethodStart() {}

        /// <summary>
        /// Скрыть диалоговое окно
        /// </summary>
        public void Hide()
        {
            _view.Hide();
            /*_currentDialogController?.OnDialogEnded();
            _currentDialogController = null;*/
        }

        /// <summary>
        /// Начать диалог с контроллером диалога
        /// </summary>
        public void StartDialog(IReadOnlyList<DialogOption> _dialogOptions)
        {
            /*_currentDialogController = dialogController;
            _currentDialogController.OnDialogStarted();*/

            Show(_dialogOptions);
        }
        
        /// <summary>
        /// Показать диалоговое окно с вариантами ответов
        /// </summary>
        private void Show(IReadOnlyList<DialogOption> options)
        {
            _view.Show(options, OnOptionSelected);
            //_view.ShowExitButton(GetExitDialogOption(), OnOptionSelected); //мб хранить в диалогах непися?
        }

        private void OnOptionSelected()
        {
            //TODO: проверять есть ли дальнейшая ветка диалога
            Hide();
        }

        private DialogOption GetExitDialogOption()
        {
            return _exitDialogOption;
        }
    }
}