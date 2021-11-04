using UnityEngine;
using System.Collections.Generic;
using RPG.Items;
using RPG.Stats;

namespace RPG.InventorySystem
{
    public class Equipment : MonoBehaviour, IModifierProvider
    {   
        [SerializeField] GameObject equipmentUI;

        Dictionary<ItemType, EquipmentSlot> slotsDictionary;

        private void Start() 
        {          
            slotsDictionary = new Dictionary<ItemType, EquipmentSlot>();
            GetAllSlots();           
        }

        private void GetAllSlots()
        {
            EquipmentSlot[] slots = equipmentUI.GetComponentsInChildren<EquipmentSlot>();
            foreach (EquipmentSlot slot in slots)
            {
                slotsDictionary[slot.GetAllowedItemType()] = slot;
            }
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach(KeyValuePair<ItemType, EquipmentSlot> slot in slotsDictionary)
            {
                yield return slot.Value.GetAdditiveModifier(stat);
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (KeyValuePair<ItemType, EquipmentSlot> slot in slotsDictionary)
            {
                yield return slot.Value.GetPercentageModifier(stat);
            }
        }
    }
}
