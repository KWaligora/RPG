using UnityEngine;
using TMPro;
using RPG.Items;

namespace RPG.InventorySystem.UI
{
    public class InventoryToolTip : MonoBehaviour
     {
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI stats;
        [SerializeField] TextMeshProUGUI description;

        private Canvas canvas;
        private bool set = false;

        private void Awake() 
        {
            canvas = GetComponent<Canvas>();
            Hide();
        }

        public void Set(ItemDataBase item)
        {
            set = true;
            itemName.text = item.GetItemName();
            description.text = item.GetDescription();
            stats.text = item.GetStats();            
        }

        public void Reset()
        {
            set = false;
        }

        public void Hide()
        {
            canvas.enabled = false;
        }

        public void Show()
        {
            if(set) canvas.enabled = true;
        }
    }
}