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

        enum CursorType
        {
            None,
            Movement,
            Combat,
            UI
        }

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

            if(InteractWithCombat()) return;
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

        private bool InteractWithCombat()
        {
            RaycastHit [] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {                
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if(target == null) continue;             
                if(!fighter.CanAttack(target.gameObject))  continue;                                       
        
                if(Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target.gameObject);                    
                }
                SetCursor(CursorType.Combat);
                return true;                
            }
            return false;
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
