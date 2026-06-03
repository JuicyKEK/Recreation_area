using System.Collections.Generic;

namespace Game.Scripts.Player.External.DialogBox.Interfaces
{
    /// <summary>
    /// Интерфейс контроллера диалога для NPC
    /// </summary>
    public interface IDialogController
    {
        /// <summary>
        /// Получить список вариантов диалога
        /// </summary>
        public IReadOnlyList<DialogOption> GetDialogOptions();
        
        /// <summary>
        /// Вызывается при начале диалога
        /// </summary>
        public void OnDialogStarted();
        
        /// <summary>
        /// Вызывается при завершении диалога
        /// </summary>
        public void OnDialogEnded();

        public void AddDialogOption(DialogOption option);

        /// <summary>
        /// Добавить вариант диалога
        /// </summary>
        public void RemoveDialogOption(DialogOption option);

        /// <summary>
        /// Очистить все варианты диалога
        /// </summary>
        public void ClearDialogOptions();
    }
}

