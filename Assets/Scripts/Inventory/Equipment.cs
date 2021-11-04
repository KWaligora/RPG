using UnityEngine;
using System.Collections.Generic;
using RPG.Items;

namespace RPG.InventorySystem
{
    public class Equipment : MonoBehaviour
    {   
        [SerializeField] GameObject equipmentUI;

        Dictionary<ItemType, EquipmentSlot> slotsDictionary;

        private void Start() 
        {
            slotsDictionary = new Dictionary<ItemType, EquipmentSlot>();
            GetAllSlots();
            Debug.Log(slotsDictionary.Count);
        }

        private void GetAllSlots()
        {
            EquipmentSlot[] slots = equipmentUI.GetComponentsInChildren<EquipmentSlot>();
            foreach (EquipmentSlot slot in slots)
            {
                slotsDictionary[slot.GetAllowedItemType()] = slot;
            }
        }
    }
}
