using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
    /// <summary>
    /// Calculate final player stats
    /// </summary>
    public class PlayerStatManager : StatManager
    {  
        AttributeManager attributeManager;

        protected override void Awake() {           
            base.Awake();

            attributeManager = GetComponent<AttributeManager>();            
        }

        private void Start() 
        {            
            Recalculate();
            attributeManager.OnChanged += Recalculate;
        }

        public void Recalculate()
        {
            SetBaseStats();

            UpdateDamage();
            UpdateHealth();         
        }

        private void UpdateDamage()
        {           
            float newflatDamage = attributeManager.GetAttribute(PlayerAttributes.strength);
            newflatDamage *= 0.2f;
            newflatDamage += GetStat(FighterStat.flatDamage);
            newflatDamage += attributeManager.GetAttribute(PlayerAttributes.Damage);
            fighterStat[FighterStat.flatDamage] = newflatDamage;
        }

        private void UpdateHealth()
        {           
            float newHealth = attributeManager.GetAttribute(PlayerAttributes.vitality);
            newHealth *= 0.3f;
            newHealth += GetStat(FighterStat.health);
            fighterStat[FighterStat.health] = newHealth;
        }
    }
}
