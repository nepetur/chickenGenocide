using System;
using UnityEngine;

namespace ChickenGenocide{
    public class ChickenShooter : MonoBehaviour{
        [Serializable] private struct Sounds{
            public AudioClip shoot, worldHit, chickenHit;
        }

        [Space, SerializeField] private Sounds sounds;

        [Space, SerializeField] private ParticleSystem hitEffect;
        
        private Camera mainCamera;
        private bool click => Input.GetMouseButtonDown(0);

        private float shootDelay;

        private void Awake(){
            mainCamera = Camera.main;
        }

        private void Update(){
            if(shootDelay > 0) shootDelay -= Time.deltaTime;

            var canShoot = shootDelay <= 0 && !BulletManager.Current.IsReloading;

            Crosshair.Current.IsActive = canShoot;

            if(click && canShoot) Shoot();
        }

        private void Shoot(){
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            var raycast = Physics.Raycast(ray, out RaycastHit hit);

            var chicken = raycast ? hit.transform.GetComponentInParent<Chicken>() : null;

            GameManager.Current.PlaySound(sounds.shoot);

            if(raycast){
                GameManager.Current.PlaySound(chicken ? sounds.chickenHit : sounds.worldHit);
            }

            BulletManager.Current.RemoveBullet();

            shootDelay = .1f;

            hitEffect.Play();

            hitEffect.transform.position = raycast ? hit.point : ray.GetPoint(15);

            string message;

            if(chicken && !chicken.IsDead){
                chicken.Die(true);

                message = "<color=green>+15";
            }
            else message = "<color=grey>мимо";

            DynamicWorldMessages.Current.ShowMessage(message, hitEffect.transform.position);
        }
    }
}