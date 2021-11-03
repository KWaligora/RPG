using UnityEngine;
using RPG.Control;
using RPG.InventorySystem;

namespace RPG.Combat
{
    public class ItemPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig item;
        [SerializeField] float HealthToRestore = 0;

        private Inventory inventory;

        private void Start() 
        {
            inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();    
        }

        private void Pickup(GameObject subject)
        {
            if(item != null)
            {              
                inventory.AddItem(item);
                Destroy(gameObject);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
