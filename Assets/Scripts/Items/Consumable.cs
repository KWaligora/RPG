using UnityEngine;

namespace RPG.Items
{    
    [CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumables/New Consumable", order = 0)]
    public class Consumable : ItemDataBase
    {
        [SerializeField] float pointsToHeal;

        public override string GetStats()
        {
            return "Points to heal:" + pointsToHeal;
        }
    }
}
