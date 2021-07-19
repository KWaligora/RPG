using UnityEngine;
using RPG.Saving;

namespace RPG.Resources
{    
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] int experiencePoints = 0;     

        public void GainExperience(int experience)
        {
            experiencePoints += experience;
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