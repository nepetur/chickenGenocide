using UnityEngine;
using System.Collections;

namespace ChickenGenocide{
    public class BulletManager : MonoBehaviour{
        public static BulletManager Current {get; private set;}

        [Space, SerializeField] private AudioClip reloadSound, removeSound;

        private int bulletCount;

        private bool HasBullets = true;

        public bool IsReloading {get; private set;}

        private void Awake(){
            Current = this;

            bulletCount = transform.childCount;
        }

        public void RemoveBullet(){
            bulletCount--;

            StartCoroutine(Removing());

            if(bulletCount == 0) StartCoroutine(Reloading());
        }

        private IEnumerator Removing(){
            var index = transform.childCount - bulletCount - 1;

            var bullet = transform.GetChild(index).GetChild(0);

            GameManager.Current.PlaySound(removeSound);

            for(float time = 0; time < 1; time += Time.deltaTime * 1.25f){
                bullet.localPosition = new Vector3(Mathf.Lerp(0, -150, time), Animations.SineInterpolation(time) * 200);

                bullet.localEulerAngles = Vector3.forward * Mathf.Lerp(0, -180, time);

                yield return null;
            }

            if(index == transform.childCount - 1) HasBullets = false;
        }

        private IEnumerator Reloading(){
            IsReloading = true;

            yield return new WaitUntil(
                () => HasBullets == false
            );

            HasBullets = true;

            for(int i = transform.childCount - 1; i >= 0; i--){
                var bullet = transform.GetChild(i).GetChild(0);

                bullet.localPosition = Vector3.zero;

                bullet.localScale = Vector3.zero;

                bullet.localEulerAngles = Vector3.zero;

                GameManager.Current.PlaySound(reloadSound);

                for(float time = 0; time <= 1; time += Time.deltaTime * 2.5f){
                    bullet.localScale = Vector3.one * Animations.EaseOutBack(time, 2);

                    yield return null;
                }
            }

            bulletCount = transform.childCount;

            IsReloading = false;
        }
    }
}