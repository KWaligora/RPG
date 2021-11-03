using UnityEngine;
using UnityEngine.EventSystems;
using RPG.InventorySystem.UI;

namespace RPG.InventorySystem
{
    public class InventorySlot : MonoBehaviour, IDropHandler
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

        public void RemoveItem()
        {
            itemIcon.RemoveIcon();
            itemData = null;
        }

        public InventoryItemIcon GetItemIcon()
        {
            return itemIcon;
        }

        public bool isEmpty()
        {
            if (itemData == null) return true;
            return false;
        }

        public void OnDrop(PointerEventData eventData)
        {
           if(eventData.pointerDrag != null)
           {
                DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();
                if(dragItem == null) return; 

                IInventoryItemData item = dragItem.GetInventorySlot().GetItem();
                if(item == null) return;                              
                
                if(isEmpty())
                { 
                    SetItem(dragItem.GetInventorySlot().GetItem());
                    dragItem.GetInventorySlot().RemoveItem();
                }          
                else
                {                  
                    Swap(dragItem.GetInventorySlot());                    
                }             
           }
        }

        private void Swap(InventorySlot slot)
        {        
            if(slot == null) return;
            IInventoryItemData itemDataTemp = slot.GetItem();
            if(itemDataTemp.Equals(this.itemData)) return;
            slot.SetItem(itemData);
            SetItem(itemDataTemp);
        }
    }
}
