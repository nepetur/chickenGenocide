using UnityEngine;
using UnityEngine.UI;

namespace ChickenGenocide{
    public class Crosshair : MonoBehaviour{
        public static Crosshair Current {get; private set;}

        public bool IsActive {get; set;} = true;

        private Image image;

        private void Awake(){
            Current = this;

            image = GetComponentInChildren<Image>();
        }

        private void OnEnable(){
            Cursor.visible = false;
        }

        private void OnDisable(){
            Cursor.visible = true;
        }

        private void Update(){
            transform.position = Input.mousePosition;

            image.color = Color.Lerp(image.color, BulletManager.Current.IsReloading ? Color.grey : Color.red, Time.deltaTime * 15);

            transform.localScale = Vector3.Lerp(
                transform.localScale, Vector3.one * (IsActive ? 1 : .5f), Time.deltaTime * 15
            );
        }
    }
}