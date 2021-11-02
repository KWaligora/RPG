using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] int inventorySize = 36;
        [SerializeField] GameObject inventorySlotPrefab;
        [SerializeField] Transform itemsContainer;

        private void Start() 
        {
            for(int i = 0; i < inventorySize; i++)
            {
                Instantiate<GameObject>(inventorySlotPrefab, itemsContainer);
            }
        }
    }
}
