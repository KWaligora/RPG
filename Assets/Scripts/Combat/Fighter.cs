using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;
using RPG.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        
        [SerializeField] private float timeBetweenAttacks = 1f;        
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        private Health target;
        private Mover mover;
        private Animator animator;
        LazyValue<Weapon> currentWeapon;

        private float timeSinceLastAttack = Mathf.Infinity;
        
        private void Awake() 
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();  

            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);          
        }

        private Weapon SetupDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        private void Start() 
        {
            currentWeapon.ForceInit();
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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
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
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);            
            if(target == null) return;  
            target.TakeDamage(gameObject, damage);
        }

        //Animation Event
        private void Shoot()
        {            
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if(target == null) return;
            currentWeapon.value.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetRange();
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

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat==Stat.Damage)
            {
                yield return currentWeapon.value.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string) state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }    
    }
}

