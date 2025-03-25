using UnityEngine;

namespace ChickenGenocide{
    public class HorizontalCamera : MonoBehaviour{
        [Space, SerializeField] private float speed, acceleration, limit;

        private float velocity;

        private void Update(){
            Move();
        }

        private void Move(){
            var mouseX = Input.mousePosition.x;
            
            const float delta = 15;

            var direction = mouseX < delta ? -1 : mouseX > Screen.width - delta ? 1 : 0;

            velocity = Mathf.Lerp(velocity, direction * speed, Time.deltaTime * acceleration);

            var x = Mathf.Clamp(transform.position.x + (velocity * Time.deltaTime), -limit, limit);

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}