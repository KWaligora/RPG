using UnityEngine;
using System.Collections.Generic;
using RPG.Stats;

namespace RPG.InventorySystem
{
    public class EquipmentSlot : ItemSlotBases
    {
        //[SerializeField] Fighter fighter;
        Transform PrefabDesintation;

        Dictionary<FighterStat, float> additiveModifiers;
        Dictionary<FighterStat, float> percentageModifiers;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Equip()
        {
         
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
    }
}
