using UnityEngine;

namespace ChickenGenocide{
    public class Crosshair : MonoBehaviour{
        public static Crosshair Current {get; private set;}

        [HideInInspector] public bool halfSized;
        [HideInInspector] public float scale = 1;

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
            transform.position = Input.mousePosition;

            transform.localScale = Vector3.Lerp(
                transform.localScale, Vector3.one * (halfSized ? .5f : 1) * scale, Time.unscaledDeltaTime * 15
            );
        }
    }
}