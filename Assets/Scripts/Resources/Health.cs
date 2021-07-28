using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;

        private bool isDead = false;

        private void Start() 
        {
            if(healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
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

        public void TakeDamage(GameObject  instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);           
            CheckDeath();           
            if(isDead) AwardExperience(instigator);
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

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100 * healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health);
        }
    }
}