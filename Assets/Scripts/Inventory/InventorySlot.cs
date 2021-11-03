using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RPG.InventorySystem.UI;

namespace RPG.InventorySystem
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        private InventoryItemIcon itemIcon;
        private IInventoryItemData itemData = null;
        private int currentStack = 0;
        private Text stack;

        private void Awake() 
        {
            itemIcon = GetComponentInChildren<InventoryItemIcon>();
            stack = GetComponentInChildren<Text>();
            stack.enabled = false;
        }

        public void SetItem(IInventoryItemData itemData)
        {
            if(itemData != null)
            {
                this.itemData = itemData;                         
                itemIcon.SetIcon(itemData.GetIcon());
                currentStack = 1;
                UpdateStack();
            }            
        }

        public void UpdateStack()
        {
            if(currentStack > 1)
            {
                stack.enabled = true;
                stack.text = currentStack.ToString();
            }
            else stack.enabled  = false;
        }

        public int GetStack()
        {
            return currentStack;
        }

        public void SetStack(int amount)
        {
            currentStack = Mathf.Min(itemData.GetMaxStack(), amount);
            UpdateStack();
        }

        // returns excess
        public int IncreasAmount(int amount)
        {
            // set new amount
            int maxStack = itemData.GetMaxStack();
            currentStack = Mathf.Min(maxStack, currentStack + amount);

            UpdateStack();

            // calculate excess
            if(maxStack >= currentStack + amount)
                return 0;
            else return maxStack - currentStack + amount;
        }

        public void DecreaseAmount(int amount)
        {
            currentStack = Mathf.Max(0, currentStack - amount);
            if(currentStack == 0) RemoveItem();
            else UpdateStack();
        }

        public IInventoryItemData GetItem()
        {
            return itemData;
        }

        public void RemoveItem()
        {
            itemIcon.RemoveIcon();
            itemData = null;
            currentStack = 0;
            UpdateStack();
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
                    SetStack(dragItem.GetInventorySlot().GetStack());
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

            IInventoryItemData itemDataTemp = slot.GetItem();

            if(itemDataTemp == itemData)
            {
                int excess = IncreasAmount(slot.GetStack());

                if(excess == 0)                
                    slot.RemoveItem();                  
                else slot.SetStack(excess);                    
                
                return;
            }
            int slotStackTemp = slot.GetStack();
            slot.SetItem(itemData);
            slot.SetStack(currentStack);            
            SetItem(itemDataTemp);
            SetStack(slotStackTemp);
        }
    }
}
