using UnityEngine;

namespace ChickenGenocide{
    public class ChickenShooter : MonoBehaviour{
        [Space, SerializeField] private AudioClip shootSound;

        [Space, SerializeField] private ParticleSystem hitEffect;
        
        private Camera mainCamera;
        private bool click => Input.GetMouseButtonDown(0);

        private float shootDelay;

        private void Awake(){
            mainCamera = Camera.main;
        }

        private void Update(){
            var canShoot = shootDelay <= 0;

            if(Crosshair.Current) Crosshair.Current.halfSized = !canShoot;

            if(canShoot){
                if(click) Shoot();
            }
            else{
                shootDelay -= Time.deltaTime;
            }
        }

        private void Shoot(){
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            var raycast = Physics.Raycast(ray, out RaycastHit hit);

            var chicken = raycast ? hit.transform.GetComponentInParent<Chicken>() : null;

            GameManager.Current.PlaySound(shootSound);

            shootDelay = .25f;

            hitEffect.Play();

            hitEffect.transform.position = raycast ? hit.point : ray.GetPoint(25);

            string message;

            if(chicken && !chicken.IsDead){
                chicken.Die();

                message = "<color=green>+15";
            }
            else message = "<color=grey>мимо";

            DynamicWorldMessages.Current.ShowMessage(message, hitEffect.transform.position);
        }
    }
}