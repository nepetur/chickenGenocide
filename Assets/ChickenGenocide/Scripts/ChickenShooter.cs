using UnityEngine;

namespace ChickenGenocide{
    public class ChickenShooter : MonoBehaviour{
        [Space, SerializeField] private AudioClip shootSound;

        private Camera mainCamera;
        private bool click => Input.GetMouseButtonDown(0);

        private float shootDelay;

        private void Awake(){
            mainCamera = Camera.main;
        }

        private void Update(){
            var canShoot = shootDelay <= 0;

            Crosshair.Current.halfSized = !canShoot;

            if(!canShoot){
                shootDelay -= Time.deltaTime;
            }
            else if(click) Shoot();
        }

        private void Shoot(){
            var raycast = Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);

            var chicken = raycast ? hit.transform.GetComponentInParent<Chicken>() : null;

            GameManager.Current.PlaySound(shootSound);

            shootDelay = 1;

            if(!chicken || chicken.isDead) return;

            chicken.Die();
        }
    }
}