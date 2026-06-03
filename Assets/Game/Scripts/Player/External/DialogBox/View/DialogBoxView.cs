using System;
using System.Collections.Generic;
using Game.Scripts.Serviсes.ObjectPool;
using UnityEngine;

namespace Game.Scripts.Player.External.DialogBox
{
    /// <summary>
    /// View компонент диалогового окна с адаптивной версткой
    /// </summary>
    public class DialogBoxView : MonoBehaviour
    {
        public bool IsVisible => _dialogPanel != null && _dialogPanel.gameObject.activeSelf;        
        
        [Header("UI Elements")]
        [SerializeField] private Transform _dialogPanel;
        [SerializeField] private DialogOptionButton _optionButtonPrefab;
        [SerializeField] private int _initialPoolSize = 3;
        
        private ObjectPool<DialogOptionButton> _buttonPool;

        public void Init()
        {
            if (_dialogPanel != null)
                _dialogPanel.gameObject.SetActive(false);
            
            InitializePool();
        }
        
        /// <summary>
        /// Показать диалоговое окно с вариантами
        /// </summary>
        public void Show(IReadOnlyList<DialogOption> options, Action onOptionSelected)
        {
            for (int i = 0; i < options.Count; i++)
            {
                var button = _buttonPool.Get();
                var option = options[i];
                
                button.Init(option.Text, () =>
                {
                    option.OnSelected?.Invoke();
                    onOptionSelected?.Invoke();
                });
            }
            
            ShowDialogPanel();
        }        
        
        /// <summary>
        /// Показать диалоговое окно с вариантами
        /// </summary>
        public void ShowExitButton(DialogOption option, Action onOptionSelected) //а нафиг оно отдельно?
        {
            var button = _buttonPool.Get();
            
            button.Init(option.Text, () =>
            {
                option.OnSelected?.Invoke();
                onOptionSelected?.Invoke();
            });
            
            ShowDialogPanel();
        }

        /// <summary>
        /// Скрыть диалоговое окно
        /// </summary>
        public void Hide()
        {
            _buttonPool.ReturnAll();
            
            if (_dialogPanel != null)
                _dialogPanel.gameObject.SetActive(false);
            
            // Вернуть курсор в игровое состояние
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void ShowDialogPanel()
        {
            if (_dialogPanel != null)
                _dialogPanel.gameObject.SetActive(true);
            
            //TODO: добавить возможность выбирать циферкой 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        private void OnDestroy()
        {
            _buttonPool?.Clear();
        }

        private void InitializePool()
        {
            _buttonPool = new ObjectPool<DialogOptionButton>(
                prefab: _optionButtonPrefab,
                parent: _dialogPanel,
                initialSize: _initialPoolSize
            );
        }
    }
}
