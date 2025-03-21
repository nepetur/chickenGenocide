using UnityEngine;

namespace ChickenGenocide{
    public class HorizontalCamera : MonoBehaviour{
        [Space, SerializeField] private float mouseDeltaX, speed, acceleration, limit;

        private float velocity;

        private void Update(){
            Move();
        }

        private void Move(){
            var mouse = Input.mousePosition;

            var direction = mouse.x < mouseDeltaX ? -1 : mouse.x > Screen.width - mouseDeltaX ? 1 : 0;

            velocity = Mathf.Lerp(velocity, direction * speed, Time.deltaTime * acceleration);

            var x = Mathf.Clamp(transform.position.x + (velocity * Time.deltaTime), -limit, limit);

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}