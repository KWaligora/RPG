using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
    /// <summary>
    /// Store FighterStats for character
    /// </summary>
    public class StatManager : MonoBehaviour
    {
        [SerializeField] float initHealth;
        [SerializeField] float initDamage;

        Dictionary<FighterStat, float> fighterStat;

        private void Awake() 
        {
            fighterStat = new Dictionary<FighterStat, float>();            
        }

        private void InitStats()
        {
            fighterStat[FighterStat.health] = initHealth;
            fighterStat[FighterStat.damage] = initDamage;
        }

        public float GetStat(FighterStat stat)
        {
            return fighterStat[stat];
        }
    }
}

