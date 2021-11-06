using UnityEngine;
using UnityEngine.UI;

namespace RPG.InventorySystem
{
    public class ItemStack : MonoBehaviour
    {
        private int currentStack = 0;
        private Text UIText;

        private void Awake() 
        {
            UIText = GetComponent<Text>();  
            UIText.enabled = false;  
        }

        public void UpdateStack()
        {
            if (currentStack > 1)
            {
                UIText.enabled = true;
                UIText.text = currentStack.ToString();
            }
            else UIText.enabled = false;
        }

        public int GetCurrentStack()
        {
            return currentStack;
        }

        public void SetCurrentStack(int amount)
        {
            currentStack = amount;
            UpdateStack();
        }

        /// <summary>
        /// Increase stack amount with maxStack check
        /// </summary>        /// 
        /// <returns>excess</returns>
        public int IncreasAmount(int amount, int itemMaxStack)
        {
            // set new amount            
            currentStack = Mathf.Min(itemMaxStack, currentStack + amount);

            UpdateStack();

            // calculate excess
            if (itemMaxStack >= currentStack + amount)
                return 0;
            else return itemMaxStack - currentStack + amount;
        }

        public void DecreaseAmount(int amount)
        {
            currentStack = Mathf.Max(0, currentStack - amount);
            UpdateStack();
        }
    }
}