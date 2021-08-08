using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] int regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;   

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>{}

        LazyValue<float> healthPoints;

        private bool isDead = false;

        private void Awake() 
        {
            healthPoints = new LazyValue<float>(GetInitialValue);
        }

        private void Start() 
        {
            healthPoints.ForceInit();               
        }

        private float GetInitialValue()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void OnEnable() 
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable() 
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        private void RegenerateHealth()
        {
            int regenHealthPoints = (int)GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage / 100;
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public object CaptureState()
        {
             return healthPoints.value;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float) state;
            CheckDeath();
        }

        public void TakeDamage(GameObject  instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);            

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            takeDamage.Invoke(damage);
                    
            CheckDeath();           
            if(isDead)
            {
                AwardExperience(instigator);
                onDie.Invoke();
            } 
        }  

        private void CheckDeath()
        {
            if (healthPoints.value <= 0 && !isDead)
            {
                isDead = true;

                GetComponent<Animator>().SetTrigger("die");  
                GetComponent<ActionScheduler>().CancelCurrentAction();          
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null) return;

            experience.GainExperience((int)GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }
    }
}