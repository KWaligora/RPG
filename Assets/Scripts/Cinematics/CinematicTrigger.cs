using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTrigged = false;

        private void OnTriggerEnter(Collider other) 
        {        
            if(other.gameObject.tag == "Player" && !alreadyTrigged)
            {
                alreadyTrigged = true;
                GetComponent<PlayableDirector>().Play();
            }                
        }
    }
} 
