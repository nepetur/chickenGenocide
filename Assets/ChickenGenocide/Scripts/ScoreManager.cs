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
            }
        }

        private TextMeshProUGUI viewer;

        private void Awake(){
            Current = this;

            viewer = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}