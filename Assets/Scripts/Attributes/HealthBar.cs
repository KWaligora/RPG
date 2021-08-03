using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Health health = null;
        [SerializeField] Canvas rootCanvas = null;

         private void Update() 
         {
            if(Mathf.Approximately(health.GetFraction(), 0))
            {
                rootCanvas.enabled = false;
                return;
            }
            rootCanvas.enabled = true;
            UpdateHealthBar();     
         }

        public void UpdateHealthBar()
        {     
            foreground.localScale = new Vector3(health.GetFraction(), 1, 1);
        }
    }
}
