using System.Collections.Generic;
using UnityEngine;

namespace RPG.InventorySystem.UI
{
    public class InventoryUI : MonoBehaviour
    {        
        [SerializeField] InventorySlot inventorySlotPrefab;
        [SerializeField] Transform itemsContainer;

        // Add slots to inventoryUI and to inventory list
        public void CreateItemSlots(int inventorySize, ref List<InventorySlot> List)
        {            
            for (int i = 0; i < inventorySize; i++)
            {
               List.Add(Instantiate<InventorySlot>(inventorySlotPrefab, itemsContainer));
            }
        }
    }
}
