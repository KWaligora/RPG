using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            CheckDeath();             
        }

        private void CheckDeath()
        {
            if (healthPoints <= 0 && !isDead)
            {
                isDead = true;
                Animator animator = GetComponent<Animator>();
                animator.SetTrigger("die");
            }
        }
    }
}