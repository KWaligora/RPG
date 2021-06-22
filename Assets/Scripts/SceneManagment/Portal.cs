using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;

        private void OnTriggerEnter(Collider other) 
        {           
            if(other.gameObject.tag == "Player")
            {
                    SceneManager.LoadScene(sceneToLoad);        
            }             
        }
    }
}
