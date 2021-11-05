using UnityEngine;
using System.Collections.Generic;
using RPG.Stats;
using RPG.Combat;
using RPG.Items;

namespace RPG.InventorySystem
{
    public class EquipmentSlot : ItemSlotBases
    {
        Dictionary<PlayerAttributes, int> itemAttributes;
        AttributeManager attributeManager;
        Fighter fighter;

        protected override void Awake()
        {
            base.Awake();

            itemAttributes = new Dictionary<PlayerAttributes, int>();
            attributeManager = GameObject.FindWithTag("Player").GetComponent<AttributeManager>();
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();

            OnItemChange += Equip;
        }

        private void Equip()
        {
            UpdateAttributes();
            
            if(itemData is WeaponConfig)
            {
                fighter.EquipWeapon(itemData as WeaponConfig);
            }

        }

        private void UpdateAttributes()
        {            
            foreach(PlayerAttributes attribute in itemAttributes.Keys)
            {
                if(itemAttributes.ContainsKey(attribute))
                {
                    attributeManager.DecreaseAttribute(attribute, itemAttributes[attribute]);
                }
            }
            itemAttributes.Clear();
            if(itemData is IEquipable)
            {
                (itemData as IEquipable).GetItemAttributes(ref itemAttributes);
                foreach (PlayerAttributes attribute in itemAttributes.Keys)
                {
                    if (itemAttributes.ContainsKey(attribute))
                    {
                        attributeManager.IncreaseAttribute(attribute, itemAttributes[attribute]);
                    }
                }
            }            
        }
    }
}
