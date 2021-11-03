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
        // maximum items of the same type in slot
        int GetMaxStack();
    }
}