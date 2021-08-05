using UnityEngine;
using System.Collections;


namespace RPG.SceneManagment
{    
    public class Fader : MonoBehaviour 
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;

        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            yield return FadeIn(2f);
        }

        public Coroutine Fade(float target, float time)
        {
            if(currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }            
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(0.5f, time);
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while(!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }       
    }
}