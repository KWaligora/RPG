using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Items;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {        
        [SerializeField] private float timeBetweenAttacks = 1f;        
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] WeaponConfig defaultWeapon = null;

        private Health target;
        private Mover mover;
        private Animator animator;
        WeaponConfig currentWeaponConfig;
        LazyValue<Weapon> currentWeapon;

        private float timeSinceLastAttack = Mathf.Infinity;
        
        private void Awake() 
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();  

            currentWeaponConfig = defaultWeapon;  
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);        
        }

        private Weapon SetupDefaultWeapon()
        {            
            return AttachWeapon(defaultWeapon);
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

            if (!GetIsInRange(target.transform))
            {
                mover.MoveTo(target.transform.position, 1f);               
            }
            else
            {
                mover.Cancel();                   
                AttackBehaviour();                
            }
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;
            if(!mover.CanMoveTo(combatTarget.transform.position) && !GetIsInRange(combatTarget.transform)) return false;
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
            if(currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }
            target.TakeDamage(gameObject, damage);
        }

        //Animation Event
        private void Shoot()
        {            
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if(target == null) return;
            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }
            currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
        }

        private bool GetIsInRange(Transform targetTranform)
        {
            return Vector3.Distance(transform.position, targetTranform.position) < currentWeaponConfig.GetRange();
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
                yield return currentWeaponConfig.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetPercentageBonus();
            }
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string) state;
            WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }    
    }
}

