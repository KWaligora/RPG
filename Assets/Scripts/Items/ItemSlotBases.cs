using UnityEngine;
using RPG.Items;
using RPG.InventorySystem.UI;

namespace RPG.InventorySystem
{
    /// <summary>
    /// Base for Item slots
    /// </summary>
    public abstract class ItemSlotBases : MonoBehaviour
    {
        private ItemDataBase itemData = null;
        private SlotItemIcon itemIcon;
        private InventoryToolTip toolTip;
    }
}
