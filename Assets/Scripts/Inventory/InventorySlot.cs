using UnityEngine;
using RPG.InventorySystem.UI;

namespace RPG.InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        private InventoryItemIcon itemIcon;
        private IInventoryItemData itemData = null;

        private void Awake() 
        {
            itemIcon = GetComponentInChildren<InventoryItemIcon>();
        }

        public void SetItem(IInventoryItemData itemData)
        {
            if(itemData != null)
            {
                this.itemData = itemData;
                itemIcon.SetIcon(itemData.GetIcon());
            }            
        }

        public IInventoryItemData GetItem()
        {
            return itemData;
        }
    }
}
