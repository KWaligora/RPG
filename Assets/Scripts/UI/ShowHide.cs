using UnityEngine;

namespace RPG.UI
{
    public class ShowHide : MonoBehaviour
    {
        [SerializeField] 
        GameObject UI;
        bool hidden = true;

        private void Start() 
        {
            UI.SetActive(false);   
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if(hidden)
                {
                    UI.SetActive(true);
                    hidden = false;
                }
                else
                {
                    UI.SetActive(false);
                    hidden = true;
                }
            }
        }
    }
}