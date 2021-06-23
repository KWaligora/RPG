using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        private bool isDead = false;

        public object CaptureState()
        {
             return healthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float) state;
            CheckDeath();
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