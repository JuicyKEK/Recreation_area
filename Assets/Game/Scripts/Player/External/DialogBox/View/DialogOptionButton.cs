using System;
using Game.Scripts.Serviсes.ObjectPool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Player.External.DialogBox
{
    /// <summary>
    /// Компонент кнопки варианта диалога
    /// </summary>
    public class DialogOptionButton : MonoBehaviour, IPoolable
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private Action _onClickAction;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        /// <summary>
        /// Инициализировать кнопку с текстом и действием
        /// </summary>
        public void Init(string text, Action onClickAction)
        {
            _text.text = text;
            _onClickAction = onClickAction;
        }

        /// <summary>
        /// Очистить кнопку при возврате в пул
        /// </summary>
        public void Clear()
        {
            _text.text = string.Empty;
            _onClickAction = null;
        }

        private void OnButtonClick()
        {
            _onClickAction?.Invoke();
        }

        public void OnGetFromPool()
        {

        }

        public void OnReturnToPool()
        {
            _text.text = string.Empty;
            _onClickAction = null;
        }
    }
}
