using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {        
        [Range(1,99)][SerializeField] int startingLevel = 1;  
        [SerializeField] CharacterClass characterClass;  
        [SerializeField] Progression progression = null;

        private void Update() 
        {
            if(gameObject.tag == "Player")
            {
                print(GetLevel());
            }    
        }

        public int GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel()
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
