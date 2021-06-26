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

        public void Spawn(Transform handTransform, Animator animator)
        {
            if(equipedPrefab != null)
            {
                Instantiate(equipedPrefab, handTransform);
            }
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }            
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}