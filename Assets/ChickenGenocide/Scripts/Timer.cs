using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace ChickenGenocide{
    public class Timer : MonoBehaviour{
        [Space, SerializeField] private float seconds;

        private float time;

        private TextMeshProUGUI viewer;

        [Space, SerializeField] private UnityEvent OnTimeEnd;

        private void Awake(){
            viewer = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable(){
            time = seconds;
        }

        private void Update(){
            time = Mathf.MoveTowards(time, 0, Time.deltaTime);

            viewer.text = time.ToString("0");

            if(time == 0){
                enabled = false;

                OnTimeEnd?.Invoke();
            }
        }
    }
}