using UnityEngine;

namespace RPG.Stats
{
    public class StatManager : MonoBehaviour
    {
        [SerializeField] FighterStat fighterStat;

        public float GetHealth()
        {
            return fighterStat.health;
        }

        public float GetDamage()
        {
            return fighterStat.damage;
        }
    }
}

