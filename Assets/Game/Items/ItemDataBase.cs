using RPG.InventorySystem;
using UnityEngine;

namespace RPG.Items
{
    public abstract class ItemDataBase : ScriptableObject, IInventoryItemData
    {
        [SerializeField] Sprite ItemIcon;
        [SerializeField] ItemPickup itemPickup;
        [SerializeField] bool stackable;

        public void DropItem()
        {
            Transform transform = GameObject.FindWithTag("Player").transform;
            Instantiate(itemPickup, transform.position, new Quaternion());
        }

        public Sprite GetIcon()
        {
            return ItemIcon;
        }

        public bool IsStackable()
        {
            return stackable;
        }
    }
}
