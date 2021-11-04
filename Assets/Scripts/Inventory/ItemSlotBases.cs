using UnityEngine;
using UnityEngine.EventSystems;
using System;
using RPG.Items;
using RPG.InventorySystem.UI;

namespace RPG.InventorySystem
{
    /// <summary>
    /// Base for Item slots
    /// </summary>
    public abstract class ItemSlotBases : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] ItemType AllowedItemType = ItemType.Any;

        public SlotItemIcon itemIcon { private set; get; }
        public ItemStack itemStack { private set; get; }
        public ItemDataBase itemData { private set; get; }

        // Fire when item in slot change
        public event Action OnItemChange;

        private InventoryToolTip toolTip;        

        protected virtual void Awake()
        {
            itemIcon = GetComponentInChildren<SlotItemIcon>();
            toolTip = GetComponentInChildren<InventoryToolTip>();
            itemStack = GetComponentInChildren<ItemStack>();

            itemData = null;            
        }

        public ItemType GetAllowedItemType()
        {
            return AllowedItemType;
        }

        public void SetItem(ItemDataBase itemData)
        {
            if (itemData != null)
            {                
                this.itemData = itemData;
                itemIcon.SetIcon(itemData.GetIcon());
                toolTip.Set(itemData);
                itemStack.SetCurrentStack(1);

                if (OnItemChange != null) OnItemChange();
            }
        }

        public void RemoveItem()
        {
            itemIcon.RemoveIcon();
            itemStack.SetCurrentStack(0);
            itemData = null;
            toolTip.Reset();

            if(OnItemChange != null) OnItemChange();
        }

        public bool isEmpty()
        {
            if (itemData == null) return true;
            return false;
        }

        private bool CheckType(ItemDataBase itemData)
        {
            if (AllowedItemType == ItemType.Any || AllowedItemType == itemData.GetItemType()) return true;
            return false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();
                if (dragItem == null) return;

                ItemDataBase item = dragItem.GetInventorySlot().itemData;
                if (item == null) return;

                if (!CheckType(item)) return;

                if (isEmpty())
                {
                    SetItem(dragItem.GetInventorySlot().itemData);
                    itemStack.SetCurrentStack(dragItem.GetInventorySlot().itemStack.GetCurrentStack());
                    dragItem.GetInventorySlot().RemoveItem();
                }
                else
                {
                    Swap(dragItem.GetInventorySlot());
                }
            }
        }

        private void Swap(ItemSlotBases slot)
        {
            if (this == slot) return;
            if (slot == null) return;

            ItemDataBase itemDataTemp = slot.itemData;

            if (itemDataTemp == itemData)
            {
                int excess = itemStack.IncreasAmount(slot.itemStack.GetCurrentStack(), slot.itemData.GetMaxStack());

                if (excess == 0)
                    slot.RemoveItem();
                else slot.itemStack.SetCurrentStack(excess);
                return;
            }
            int slotStackTemp = slot.itemStack.GetCurrentStack();
            slot.SetItem(itemData);
            slot.itemStack.SetCurrentStack(itemStack.GetCurrentStack());
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
