using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equipedPrefab = null;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            if(equipedPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHandTransform, leftHandTransform);

                Instantiate(equipedPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }            
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

        public void LaunchProjectile(Transform rightHandTranform, Transform leftHandTransform, Health target)
        {
            Transform handTransform = GetHandTransform(rightHandTranform, leftHandTransform);
            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(target);
        }
    }
}