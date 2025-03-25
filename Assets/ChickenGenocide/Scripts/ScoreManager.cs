using TMPro;
using UnityEngine;

namespace ChickenGenocide{
    public class ScoreManager : MonoBehaviour{
        public static ScoreManager Current {get; private set;}

        private int score = 0;
        public int Score{
            get => score;

            set{
                score = value;

                viewer.text = score.ToString();

                animation = 1;

                enabled = true;
            }
        }

        private new float animation;

        private TextMeshProUGUI viewer;

        private void Awake(){
            Current = this;

            viewer = GetComponentInChildren<TextMeshProUGUI>();
        }


        private void Update(){
            animation = Mathf.MoveTowards(animation, 0, Time.deltaTime * 2.5f);

            var scale = 1.35f - .35f * Mathf.Cos(2 * Mathf.PI * animation);

            viewer.transform.localScale = Vector3.one * scale;

            if(animation == 0) enabled = false;
        }
    }
}