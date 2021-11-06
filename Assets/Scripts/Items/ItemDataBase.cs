using UnityEngine;

namespace RPG.Items
{
    public abstract class ItemDataBase : ScriptableObject
    {
        [SerializeField] Sprite ItemIcon;
        [SerializeField] ItemPickup itemPickup;
        [SerializeField] int MaxStack = 1;
        [SerializeField] string itemName;
        [SerializeField] string description;
        [SerializeField] ItemType itemType;

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

        public string GetItemName()
        {
            return itemName;
        }

        public string GetDescription()
        {
            return description;
        }

        public ItemType GetItemType()
        {
            return itemType;
        }

        public abstract string GetStats();
    }
}
