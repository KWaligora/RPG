using RPG.InventorySystem;
using UnityEngine;

namespace RPG.Items
{
    public abstract class ItemDataBase : ScriptableObject, IInventoryItemData
    {
        [SerializeField] Sprite ItemIcon;
        [SerializeField] ItemPickup itemPickup;
        [SerializeField] int MaxStack = 1;
        [SerializeField] string itemName;
        [SerializeField] string description;

        public void DropItem()
        {
            Transform transform = GameObject.FindWithTag("Player").transform;
            Instantiate(itemPickup, transform.position, new Quaternion());
        }

        public Sprite GetIcon()
        {
            return ItemIcon;
        }

        public int GetMaxStack()
        {
           return MaxStack;
        }

    }
}
