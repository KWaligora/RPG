using UnityEngine;
using System.Collections.Generic;
using RPG.Items;
using RPG.Combat;

namespace RPG.InventorySystem
{
    public class Equipment : MonoBehaviour
    {   
        [SerializeField] GameObject equipmentUI;

        Dictionary<ItemType, EquipmentSlot> slotsDictionary;
        Fighter fighter;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
        }

        private void Start() 
        {          
            slotsDictionary = new Dictionary<ItemType, EquipmentSlot>();
            GetAllSlots();

            slotsDictionary[ItemType.Weapon].OnItemChange += EquipWeapon;      
        }

        private void GetAllSlots()
        {
            EquipmentSlot[] slots = equipmentUI.GetComponentsInChildren<EquipmentSlot>();
            foreach (EquipmentSlot slot in slots)
            {
                slotsDictionary[slot.GetAllowedItemType()] = slot;
            }
        }

        private void EquipWeapon()
        {
            if(slotsDictionary[ItemType.Weapon].isEmpty())
            {
                print("Empty");
                fighter.SetupDeafaultWeapon();
            }
            else
            {
                ItemDataBase item = slotsDictionary[ItemType.Weapon].itemData;
                if (item is WeaponConfig)
                {
                    fighter.EquipWeapon(item as WeaponConfig);
                }
            }            
        }
    }
}
