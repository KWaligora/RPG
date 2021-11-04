using UnityEngine;

namespace RPG.InventorySystem
{
    public class EquipmentSlot : ItemSlotBases
    {
        Transform PrefabDesintation;

        private void Start() {
            itemChange.AddListener(Equip);
        }

        private void Equip()
        {
            Debug.Log(itemData.GetType());
        }
    }
}
