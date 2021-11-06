using UnityEngine;
using System;
using System.Collections.Generic;

namespace RPG.Stats
{
    /// <summary>
    /// Store player attributes from lvl progress and eq
    /// </summary>
    public class AttributeManager : MonoBehaviour
    {
        [SerializeField] int baseStrength;
        [SerializeField] int baseDexterity;
        [SerializeField] int baseVitality;
        [SerializeField] int baseEnergy;

        public event Action OnChanged;

        private Dictionary<PlayerAttributes, int> attributes;

        private void Awake() 
        {
            attributes = new Dictionary<PlayerAttributes, int>();
            SetBaseAttributes();              
        }

        private void SetBaseAttributes()
        {
            attributes[PlayerAttributes.strength] = baseStrength;
            attributes[PlayerAttributes.dexterity] = baseDexterity;
            attributes[PlayerAttributes.vitality] = baseVitality;
            attributes[PlayerAttributes.energy] = baseEnergy;
            attributes[PlayerAttributes.Damage] = 0;
        }

        public int GetAttribute(PlayerAttributes attribute)
        {
            if(attributes.ContainsKey(attribute))
                return attributes[attribute];
            else return 0;
        }

        public void IncreaseAttribute(PlayerAttributes attribute, int value)
        {
            attributes[attribute] += value;
            OnChanged?.Invoke();
        }

        public void DecreaseAttribute(PlayerAttributes attribute, int value)
        {
            attributes[attribute] -= value;
            OnChanged?.Invoke();
        }
    }
}
