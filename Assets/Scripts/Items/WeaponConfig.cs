using RPG.Attributes;
using UnityEngine;
using RPG.Combat;
using RPG.InventorySystem;
using System.Collections.Generic;
using RPG.Stats;

namespace RPG.Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapons/New Weapon", order = 0)]
    public class WeaponConfig : ItemDataBase, IEquipable
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] Weapon equipedPrefab = null;
        [SerializeField] private int weaponDamage = 5;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        Dictionary<FighterStat, float> Modifiers;
        const string weaponName = "Weapon";

        public Weapon Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            Weapon weapon = null;
            if(equipedPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHandTransform, leftHandTransform);

                weapon = Instantiate(equipedPrefab, handTransform);
                weapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {           
                 animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;                
            }
 
            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform oldWeapon = rightHandTransform.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHandTransform.Find(weaponName);
            }
            if(oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetHandTransform(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHandTransform;
            else handTransform = leftHandTransform;
            return handTransform;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }

        public bool HasProjectal()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHandTranform, Transform leftHandTransform, Health target, GameObject instigator, float calculatedDamage)
        {
            Transform handTransform = GetHandTransform(rightHandTranform, leftHandTransform);
            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public override string GetStats()
        {
            return "weaponDamage: " + weaponDamage + "\n weaponRange: " + weaponRange;
        }

        public void GetItemAttributes(ref Dictionary<PlayerAttributes, int> itemAttributes)
        {
            itemAttributes[PlayerAttributes.Damage] = weaponDamage;
        }
    }
}