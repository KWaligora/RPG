using System.Collections.Generic;
using UnityEngine;
using RPG.InventorySystem.UI;
using RPG.Items;

namespace RPG.InventorySystem
{
    public class Inventory : MonoBehaviour
    {        
        [SerializeField] InventoryUI inventoryUI;
        [SerializeField] int inventorySize = 36;

        // Container for inventory items
        private List<InventorySlot> inventorySlots;

        private void Awake() 
        {
            inventorySlots = new List<InventorySlot>();    
        }

        private void Start()
        {
            inventoryUI.CreateItemSlots(inventorySize, ref inventorySlots);
        }

        public bool AddItem(ItemDataBase itemData)
        {
            if(itemData.GetMaxStack() > 1)
                if(TryStack(itemData)) return true;

            InventorySlot slot = GetFirstEmptySlot();

            if(slot != null)
            {
                slot.SetItem(itemData);
                return true;
            }
            return false;
        }

        private bool TryStack(ItemDataBase itemData)
        {
            int itemMaxStack = itemData.GetMaxStack();
            int excess;

            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.itemData == itemData)
                {      
                    excess = slot.itemStack.IncreasAmount(1, itemData.GetMaxStack());
                    if(excess == 0) return true;
                }              
            }        
            return false;
        }

        private InventorySlot GetFirstEmptySlot()
        {
            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot.isEmpty())
                {
                    return slot;
                }
            }
            return null;
        }
    }
}
