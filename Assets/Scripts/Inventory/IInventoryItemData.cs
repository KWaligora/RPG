using UnityEngine;
/// <summary>
/// For all items that can be placed in inventory
/// </summary>

namespace RPG.InventorySystem
{
    public interface IInventoryItemData
    {
        Sprite GetIcon();
        void DropItem();
        bool IsStackable();
    }
}