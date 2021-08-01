using System;
using GameDevTV.Utils;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {        
        [Range(1,99)][SerializeField] int startingLevel = 1;  
        [SerializeField] CharacterClass characterClass;  
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject LevelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        public event Action onLevelUp;

        LazyValue<int> currentLevel;
        Experience experience;

        private void Awake() 
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(GetInitialValue);
        }

        private void Start() 
        {
            currentLevel.ForceInit();
        }

        private int GetInitialValue()
        {
            return CalculateLevel();
        }

        private void OnEnable() 
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable() 
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }  
        }

        private void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(LevelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat)/100);
        }      

        private int GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if(!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public int GetLevel()
        {
            if(currentLevel.value < 1)
            {
                currentLevel.value = CalculateLevel();
            }
            return currentLevel.value;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if(experience == null) return startingLevel;

            int currentXP = GetComponent<Experience>().GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExpierenceToLevelUp, characterClass); //penultimate - przedostatni
            for(int level = 1; level < penultimateLevel; level++)
            {
                int XPToLevelUp = progression.GetStat(Stat.ExpierenceToLevelUp, characterClass, level);
                if(XPToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevel;
        }
    }
}
