using System.Collections;
using UnityEngine;
using RPG.Control;
using RPG.InventorySystem;

namespace RPG.Combat
{
    public class ItemPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig item;
        [SerializeField] float HealthToRestore = 0;
        [SerializeField] float respawnTime = 5;
        [SerializeField] Inventory inventory;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.tag == "Player")
            {               
                Pickup(other.gameObject);
            }
        }

        private void Pickup(GameObject subject)
        {
            if(item != null)
            {              
                inventory.AddItem(item);
            }
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
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
