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
        [SerializeField] float initArmor;

        protected Dictionary<FighterStat, float> fighterStat;

        protected virtual void Awake() 
        {
            fighterStat = new Dictionary<FighterStat, float>();
            SetBaseStats();
        }

        protected void SetBaseStats()
        {
            fighterStat[FighterStat.health] = initHealth;
            fighterStat[FighterStat.flatDamage] = initDamage;
            fighterStat[FighterStat.armor] = initArmor;
        }

        public float GetStat(FighterStat stat)
        {
            if(fighterStat.ContainsKey(stat))
                return fighterStat[stat];
            else return 0;
        }
    }
}

