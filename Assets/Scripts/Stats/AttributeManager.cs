using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
    public class AttributeManager : MonoBehaviour
    {
        [SerializeField] int initStrength;
        [SerializeField] int initDexterity;
        [SerializeField] int initVitality;
        [SerializeField] int initEnergy;

        private Dictionary<PlayerAttributes, int> attributes;

        private void Awake() 
        {
            attributes = new Dictionary<PlayerAttributes, int>();
            InitAttributes();              
        }

        private void InitAttributes()
        {
            attributes[PlayerAttributes.strength] = initStrength;
            attributes[PlayerAttributes.dexterity] = initDexterity;
            attributes[PlayerAttributes.vitality] = initVitality;
            attributes[PlayerAttributes.energy] = initEnergy;
        }
    }
}
