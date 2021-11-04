using UnityEngine;
using UnityEngine.UI;

namespace RPG.InventorySystem.UI
{
    [RequireComponent(typeof(Image))]
    public class SlotItemIcon : MonoBehaviour
    {
        private Image CurrentIcon;   

        private void Awake()
        {
            CurrentIcon = GetComponent<Image>();             
        }

        public void SetIcon(Sprite icon)
        {
            if(icon != null)
            {
                CurrentIcon.sprite = icon;
                CurrentIcon.color = new Color(255, 255, 255, 255);
            }               
        }

        public void RemoveIcon()
        {
            CurrentIcon.sprite = null;
            CurrentIcon.color = new Color(255, 255, 255, 0);
        }
    }
}
