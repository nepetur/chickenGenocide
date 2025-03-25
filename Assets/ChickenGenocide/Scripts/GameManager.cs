using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChickenGenocide{
    [CreateAssetMenu] public class GameManager : ScriptableObject{
        public static GameManager Current {get; private set;}

        private GameComponents gameComponents;

        [
            RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)
        ]
        private static void Init(){
            Current = Resources.Load<GameManager>("GameManager");

            var gameComponentsPrefab = Resources.Load<GameComponents>("GameComponents");

            Current.gameComponents = Instantiate(gameComponentsPrefab);

            #if UNITY_EDITOR
            Current.gameComponents.name = gameComponentsPrefab.name;
            #endif

            DontDestroyOnLoad(Current.gameComponents);
        }

        public void PlaySound(AudioClip audioClip) => gameComponents.AudioSource.PlayOneShot(audioClip);

        public bool Paused{
            get => Time.timeScale == 0;
            
            set{
                Time.timeScale = value ? 0 : 1;
            }
        }

        public void Pause() => Paused = true;

        public void Resume() => Paused = false;

        public void Quit() => Application.Quit();

        public void LoadScene(string scene) => SceneTransition.Current.ChangeScene(scene);
    }
}