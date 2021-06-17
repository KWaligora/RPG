using UnityEngine;

namespace RPG.Core
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
            CheckDeath();             
        }

        private void CheckDeath()
        {
            if (healthPoints <= 0 && !isDead)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");  
                GetComponent<ActionScheduler>().CancelCurrentAction();          
            }
        }
    }
}