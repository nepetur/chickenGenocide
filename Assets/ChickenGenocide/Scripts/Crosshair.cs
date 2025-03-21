using UnityEngine;

namespace ChickenGenocide{
    public class Crosshair : MonoBehaviour{
        public static Crosshair Current {get; private set;}
        public bool halfSized;

        private RectTransform rectTransform => transform as RectTransform;

        private bool active{
            set{
                Cursor.visible = !value;
            }
        }

        private void Awake(){
            Current = this;
        }

        private void OnEnable() => active = true;

        private void OnDisable() => active = false;

        private void Update(){
            rectTransform.position = Input.mousePosition;

            rectTransform.localScale = Vector3.Lerp(
                rectTransform.localScale, Vector3.one * (halfSized ? .5f : 1), Time.unscaledDeltaTime * 15
            );
        }
    }
}