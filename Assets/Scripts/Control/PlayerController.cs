using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Fighter fighter;
        private Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

        private void Awake() 
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>(); 
        }

        private void Update()
        {
            if(InteractWithUI()) return;

            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            } 

            if(InteractWithComponent()) return;
            if(InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }            
            return false;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if(raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distance = new float[hits.Length];
            for(int i=0; i< hits.Length; i++)
            {
                distance[i] = hits[i].distance;
            }
            Array.Sort(distance, hits);
            return hits;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mappings = GetCursorMapping(type);
            Cursor.SetCursor(mappings.texture, mappings.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type) return mapping;
            }
            return cursorMappings[0];
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if(hasHit)
            {   if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point, 1f);                   
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }
    }
}
