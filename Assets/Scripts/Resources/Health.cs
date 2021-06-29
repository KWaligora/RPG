using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        private bool isDead = false;

        private void Start() 
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

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

        public float GetPercentage()
        {
            return 100 * healthPoints / GetComponent<BaseStats>().GetHealth();
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