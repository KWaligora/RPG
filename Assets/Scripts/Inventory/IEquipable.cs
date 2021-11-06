using System.Collections.Generic;
using RPG.Stats;

namespace RPG.InventorySystem
{
    public interface IEquipable
    {
        void GetItemAttributes(ref Dictionary<PlayerAttributes, int> itemAttributes);      
    }
}