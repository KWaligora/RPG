using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RPG.InventorySystem.UI;
using RPG.Items;

namespace RPG.InventorySystem
{
    /// <summary>
    /// Slot for any type of item
    /// </summary>
    public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] ItemType AllowedItemType = ItemType.Any;
        private SlotItemIcon itemIcon;
        private ItemDataBase itemData = null;
        private InventoryToolTip toolTip;
        private ItemStack itemStack;

        private void Awake() 
        {
            itemIcon = GetComponentInChildren<SlotItemIcon>();
            toolTip = GetComponentInChildren<InventoryToolTip>();
            itemStack = GetComponentInChildren<ItemStack>();
        }

        public void SetItem(ItemDataBase itemData)
        {
            if(itemData != null)
            {
                this.itemData = itemData;                         
                itemIcon.SetIcon(itemData.GetIcon());
                toolTip.Set(itemData);
                itemStack.SetCurrentStack(1);
            }            
        }

        public ItemStack GetStack()
        {
            return itemStack;
        }

        private bool CheckType(ItemDataBase itemData)
        {
            if(AllowedItemType == ItemType.Any || AllowedItemType == itemData.GetItemType()) return true;
            return false;
        }

        public ItemDataBase GetItem()
        {
            return itemData;
        }

        public void RemoveItem()
        {
            itemIcon.RemoveIcon();
            itemStack.SetCurrentStack(0);
            itemData = null;            
            toolTip.Reset();
        }

        public SlotItemIcon GetItemIcon()
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

                ItemDataBase item = dragItem.GetInventorySlot().GetItem();
                if(item == null) return;                              
                
                if(!CheckType(item)) return;

                if(isEmpty())
                { 
                    SetItem(dragItem.GetInventorySlot().GetItem());
                    itemStack.SetCurrentStack(dragItem.GetInventorySlot().GetStack().GetCurrentStack());
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
            if (this == slot) return;
            if(slot == null) return;

            ItemDataBase itemDataTemp = slot.GetItem();

            if(itemDataTemp == itemData)
            {
                int excess = itemStack.IncreasAmount(slot.GetStack().GetCurrentStack(), slot.GetItem().GetMaxStack());

                if(excess == 0)                
                    slot.RemoveItem();                  
                else slot.GetStack().SetCurrentStack(excess);
                return;
            }
            int slotStackTemp = slot.GetStack().GetCurrentStack();
            slot.SetItem(itemData);
            slot.GetStack().SetCurrentStack(itemStack.GetCurrentStack());            
            SetItem(itemDataTemp);
            itemStack.SetCurrentStack(slotStackTemp);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {           
            toolTip.Show();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            toolTip.Hide();
        }
    }
}
