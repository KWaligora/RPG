using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 5f;

        private Health target;
        private Mover mover;
        private Animator animator;

        private float timeSinceLastAttack = 0;
        
        private void Start() {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            //if(target != null && !GetIsInRange()) //moving only with pressed MB

            if(target == null) return;
            if(target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);               
            }
            else
            {
                mover.Cancel();                   
                AttackBehaviour();                
            }
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform, Vector3.up);

            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                //this will trigger the hit event.
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;                
            }
        }

        //Animation Event
        private void Hit()
        {           
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget){   
            GetComponent<ActionScheduler>().StartAction(this);         
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}

