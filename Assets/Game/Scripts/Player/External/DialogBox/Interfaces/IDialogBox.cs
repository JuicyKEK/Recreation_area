using System.Collections.Generic;

namespace Game.Scripts.Player.External.DialogBox.Interfaces
{
    /// <summary>
    /// Интерфейс диалогового окна
    /// </summary>
    public interface IDialogBox
    {
        /// <summary>
        /// Показать диалоговое окно с вариантами ответов
        /// </summary>
        public void StartDialog(IReadOnlyList<DialogOption> _dialogOptions);
        
        /// <summary>
        /// Скрыть диалоговое окно
        /// </summary>
        public void Hide();
        
        /// <summary>
        /// Проверка, открыто ли диалоговое окно
        /// </summary>
        public bool IsVisible { get; }
    }
}