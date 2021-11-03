using UnityEngine;
using UnityEngine.EventSystems;
using RPG.InventorySystem.UI;
using RPG.Control;

namespace RPG.InventorySystem
{
    public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Transform parentCanvas;
        private Transform orginalParent;
        private InventorySlot inventorySlot;
        private InventoryItemIcon icon;
        private IInventoryItemData itemData;
        private CanvasGroup canvasGroup;
        private PlayerController playerController;

        private void Awake() 
        {
            parentCanvas = GetComponentInParent<Canvas>().transform;
            orginalParent = gameObject.transform;
            inventorySlot = GetComponent<InventorySlot>();
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public InventorySlot GetInventorySlot()
        {
            return inventorySlot;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            icon = inventorySlot.GetItemIcon();
            if(icon != null)
            {
                icon = inventorySlot.GetItemIcon();
                playerController.enabled = false;
                itemData = inventorySlot.GetItem();
                icon.transform.SetParent(parentCanvas, false);
                Debug.Log(icon.transform.childCount);
                icon.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }           
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(icon != null)
            {
                icon.transform.position = eventData.position;
            }           
        }

        public void OnEndDrag(PointerEventData eventData)
        {                                     
            if(icon != null)
            {
                icon.transform.SetParent(orginalParent);
                icon.transform.position = orginalParent.position;
                icon.GetComponent<CanvasGroup>().blocksRaycasts = true;
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    itemData.DropItem();
                    inventorySlot.RemoveItem();
                    itemData = null;
                }
            }
            playerController.enabled = true;
            Debug.Log(icon.transform.childCount);                           
        }
    }
}
