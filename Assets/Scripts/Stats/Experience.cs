using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{    
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] int experiencePoints = 0;     
        
        public event Action onExperienceGained;

        public void GainExperience(int experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public int GetPoints()
        {
            return experiencePoints;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (int)state;
        }
    }    
}