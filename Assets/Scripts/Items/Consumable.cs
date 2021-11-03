using UnityEngine;
using RPG.InventorySystem;

namespace RPG.Items
{    
    [CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumables/New Consumable", order = 0)]
    public class Consumable : ScriptableObject {

        [SerializeField] Sprite ItemIcon;
        [SerializeField] ItemPickup itemPickup;
    }
}
