using UnityEngine;

namespace RPG.UI
{
    public class ShowHide : MonoBehaviour
    {
        [SerializeField] CanvasGroup UI;
        bool hidden = true;

        private void Start() 
        {
           UI = GetComponent<CanvasGroup>();
           Hide();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
              if(hidden) Show();
              else Hide();
            }
        }

        private void Hide()
        {
            UI.alpha = 0;
            UI.blocksRaycasts = false;
            hidden = true;
        }

        private void Show()
        {
            UI.alpha = 1;
            UI.blocksRaycasts = true;
            hidden = false;
        }
    }
}