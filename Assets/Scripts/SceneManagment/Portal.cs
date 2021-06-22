using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;

        private void OnTriggerEnter(Collider other) 
        {           
            if(other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }             
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            
            yield return SceneManager.LoadSceneAsync(sceneToLoad);   
            
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);       
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}
