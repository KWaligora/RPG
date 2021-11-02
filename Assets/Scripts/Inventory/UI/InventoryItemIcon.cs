using UnityEngine;
using UnityEngine.UI;

namespace RPG.InventorySystem.UI
{
    [RequireComponent(typeof(Image))]
    public class InventoryItemIcon : MonoBehaviour
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
    }
}
