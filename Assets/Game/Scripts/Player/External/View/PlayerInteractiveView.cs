using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Player.View
{
    public class PlayerInteractiveView : MonoBehaviour
    {
        [SerializeField] private Image m_InteractiveImage;
        [Header("Interact Color")]
        [SerializeField] private Color m_InteractColor;
        [SerializeField] private Color m_NonInteractColor;

        public void ShowInteractiveImage(bool isCanInteract)
        {
            m_InteractiveImage.gameObject.SetActive(true);
            
            m_InteractiveImage.color = isCanInteract ? m_InteractColor : m_NonInteractColor;
        }
    }
}