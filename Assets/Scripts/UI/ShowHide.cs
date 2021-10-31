using UnityEngine;

namespace RPG.UI
{
    public class ShowHide : MonoBehaviour
    {
        [SerializeField] 
        GameObject UI;
        bool Hidden = true;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if(Hidden)
                {
                    UI.SetActive(true);
                    Hidden = false;
                }
                else
                {
                    UI.SetActive(false);
                    Hidden = true;
                }
            }
        }
    }
}