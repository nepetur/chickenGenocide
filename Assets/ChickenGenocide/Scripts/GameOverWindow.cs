using TMPro;
using UnityEngine;

namespace ChickenGenocide{
    public class GameOverWindow : MonoBehaviour{
        [Space, SerializeField, TextArea] private string prompt;

        [Space, SerializeField] private TextMeshProUGUI scoresViewer;

        private CanvasGroup canvasGroup;

        private bool visible;

        private float desiredAnimation => visible ? 1 : 0;

        private new float animation;

        private void Awake(){
            canvasGroup = GetComponentInChildren<CanvasGroup>();

            visibility = desiredAnimation;
        }

        private void Update(){
            animation = Mathf.MoveTowards(animation, desiredAnimation, Time.unscaledDeltaTime * 2);

            visibility = animation;

            if(animation == desiredAnimation) enabled = false;
        }

        public void Show(){
            visible = true;

            canvasGroup.interactable = true;

            scoresViewer.text = string.Format(prompt, ScoreManager.Current.Score);

            enabled = true;
        }

        private float visibility{
            set{
                canvasGroup.alpha = value;

                transform.localScale = Vector3.one * Animations.EaseOutBack(value, 2);
            }
        }
    }
}