using UnityEngine;
using System.Collections.Generic;
using RPG.Stats;

namespace RPG.InventorySystem
{
    public class EquipmentSlot : ItemSlotBases
    {
        //[SerializeField] Fighter fighter;
        Transform PrefabDesintation;

        Dictionary<Stat, float> additiveModifiers;
        Dictionary<Stat, float> percentageModifiers;

        protected override void Awake()
        {
            base.Awake();

            OnItemChange += Equip;
            additiveModifiers = new Dictionary<Stat, float>();
            percentageModifiers = new Dictionary<Stat, float>();
        }

        private void Equip()
        {
            if(itemData is IEquipable)
                SetModifiers(itemData as IEquipable);
            else ResetModifiers();
            // if(itemData is WeaponConfig)
            // {
            //     WeaponConfig weapon = itemData as WeaponConfig;
            //     fighter.EquipWeapon(weapon);
            // }
            // if(itemData == null)
            // {
                
            // }
        }

        private void SetModifiers(IEquipable item)
        {            
            item.GetAdditiveModifiers(ref additiveModifiers);
            item.GetPercentageModifiers(ref percentageModifiers);
        }

        private void ResetModifiers()
        {
            additiveModifiers.Clear();
            percentageModifiers.Clear();
        }

        public float GetAdditiveModifier(Stat stat)
        {            
            if(additiveModifiers.ContainsKey(stat))
                return additiveModifiers[stat];
            else return 0;
        }

        public float GetPercentageModifier(Stat stat)
        {
            if (percentageModifiers.ContainsKey(stat))
                return percentageModifiers[stat];
            else return 0;
        }
    }
}
