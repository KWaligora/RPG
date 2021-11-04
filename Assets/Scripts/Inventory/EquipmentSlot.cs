using UnityEngine;

namespace RPG.InventorySystem
{
    public class EquipmentSlot : ItemSlotBases
    {
        //[SerializeField] Fighter fighter;
        Transform PrefabDesintation;

        private void Start() {
            itemChange.AddListener(Equip);
        }

        private void Equip()
        {
            // if(itemData is WeaponConfig)
            // {
            //     WeaponConfig weapon = itemData as WeaponConfig;
            //     fighter.EquipWeapon(weapon);
            // }
            // if(itemData == null)
            // {
                
            // }
        }
    }
}
