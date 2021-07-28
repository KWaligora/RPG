using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {        
        [Range(1,99)][SerializeField] int startingLevel = 1;  
        [SerializeField] CharacterClass characterClass;  
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject LevelUpParticleEffect = null;

        public event Action onLevelUp;

        int currentLevel = 0;

        private void Start() 
        {
            currentLevel =  CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }    
        }

        private void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(LevelUpParticleEffect, transform);
        }

        public int GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel()
        {
            if(currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
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
