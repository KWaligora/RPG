using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,B,C,D,E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float FadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other) 
        {           
            if(other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }             
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }           

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);   
            
            wrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(FadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);       
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                if(portal.destination != destination) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}
