using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace ChickenGenocide{
    public class SceneTransition : MonoBehaviour{
        public static SceneTransition Current {get; private set;}

        private bool changing;

        [Space, SerializeField] private CanvasGroup loadingScreen;

        private void Awake(){
            Current = this;

            loadingScreen.gameObject.SetActive(false);
        }

        public void ChangeScene(string scene){
            if(changing) return;

            StartCoroutine(
                SceneChanging(scene)
            );
        }

        private IEnumerator SceneChanging(string scene){
            changing = true;

            yield return ScreenFade(false);

            yield return new WaitForSecondsRealtime(1);

            yield return SceneManager.LoadSceneAsync(scene);

            yield return ScreenFade(true);

            changing = false;
        }

        private IEnumerator ScreenFade(bool value){
            if(!value) loadingScreen.gameObject.SetActive(true);

            float start = value ? 0 : 1, end = value ? 1 : 0;

            for(var a = start; a != end; a = Mathf.MoveTowards(a, end, Time.unscaledDeltaTime * 2)){
                fade = Animations.EaseInOutSine(a);
                
                yield return null;
            }

            fade = end;

            if(value) loadingScreen.gameObject.SetActive(false);
        }

        private float fade{
            set{
                loadingScreen.alpha = 1 - value;

                loadingScreen.transform.localScale = Vector3.one * (1 + .5f * value);
            }
        }
    }
}