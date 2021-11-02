using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory.UI
{
    [RequireComponent(typeof(Image))]
    public class InventoryIcon : MonoBehaviour
    {
        [SerializeField] Sprite ItemIcon;
        Image CurrentIcon;

        private void Awake()
        {
            CurrentIcon = GetComponent<Image>();
        }

        private void SetIcon(Sprite Icon)
        {
            if(Icon != null)
            {
                CurrentIcon.sprite = Icon;
            }
        }
    }
}
