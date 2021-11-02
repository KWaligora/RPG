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
            inventorySlots[0].SetItem(itemData);

            return true;
        }
    }
}
