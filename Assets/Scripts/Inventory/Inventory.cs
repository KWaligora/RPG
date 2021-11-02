using System.Collections.Generic;
using UnityEngine;
using RPG.InventorySystem.UI;

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

        public bool AddItem(IInventoryItemData itemData)
        {            
            InventorySlot slot = GetFirstEmptySlot();

            if(slot != null)
            {
                slot.SetItem(itemData);
                return true;
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
