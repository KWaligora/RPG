using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
    public class StatManager : MonoBehaviour
    {
        [SerializeField] float initHealth;
        [SerializeField] float initDamage;

        Dictionary<FighterStat, float> fighterStat;

        private void Awake() 
        {
            fighterStat = new Dictionary<FighterStat, float>();
            fighterStat[FighterStat.health] = initHealth;
            fighterStat[FighterStat.damage] = initDamage;

        }

        public float GetStat(FighterStat stat)
        {
            return fighterStat[stat];
        }
    }
}

