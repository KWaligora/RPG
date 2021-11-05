using System.Collections.Generic;
using RPG.Stats;

namespace RPG.InventorySystem
{
    public interface IEquipable
    {
        void GetAdditiveModifiers(ref Dictionary<FighterStat, float> modifiers);
        void GetPercentageModifiers(ref Dictionary<FighterStat, float> modifiers);
    }
}