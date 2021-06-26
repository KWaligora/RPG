using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        
        [SerializeField] private float timeBetweenAttacks = 1f;        
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;

        private Health target;
        private Mover mover;
        private Animator animator;

        private float timeSinceLastAttack = Mathf.Infinity;
        
        private void Start() 
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();  
            SpawnWeapon();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            //if(target != null && !GetIsInRange()) //moving only with pressed MB

            if(target == null) return;
            if(target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);               
            }
            else
            {
                mover.Cancel();                   
                AttackBehaviour();                
            }
        }

        private void SpawnWeapon()
        {           
            if(weapon == null) return;
            else weapon.Spawn(handTransform, animator);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }


        private void AttackBehaviour()
        {
            transform.LookAt(target.transform, Vector3.up);

            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                //this will trigger the hit event.
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        //Animation Event
        private void Hit()
        {        
            if(target == null) return;  
            target.TakeDamage(weapon.GetDamage());
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.GetRange();
        }

        public void Cancel()
        {
            target = null;
            StopAttack();
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}

