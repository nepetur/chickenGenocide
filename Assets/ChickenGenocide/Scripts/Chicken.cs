using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace ChickenGenocide{
    public class Chicken : MonoBehaviour{
        private static AnimationClip[] animations;

        private static AudioClip deathSound;
        
        public bool IsDead {get; private set;}

        private AnimationClip randomAnimation{
            get{
                var index = Random.Range(0, animations.Length);

                return animations[index];
            }
        }
        private static AnimationClip flyingAnimation;

        [Space, SerializeField] private Animator animator;

        [
            RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)
        ]
        private static void Init(){
            flyingAnimation = Resources.Load<AnimationClip>("Flying");

            deathSound = Resources.Load<AudioClip>("DeathSound");

            animations = Resources.LoadAll<AnimationClip>("ChickenAnimations");
        }

        private void Update(){
            transform.localPosition -= transform.forward * Time.deltaTime * 2;
        }

        public void Spawn(){
            if(!enabled) transform.localEulerAngles = Vector3.zero;

            transform.GetChild(0).localEulerAngles = new Vector3(enabled ? 90 : 0, 180);

            gameObject.SetActive(true);

            IsDead = false;

            SetAnimations();

            StartCoroutine(Appear(true));
        }

        private void SetAnimations(){
            var aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);

            var o = new KeyValuePair<AnimationClip, AnimationClip>[1];

            var a = enabled ? flyingAnimation : randomAnimation;

            o[0] = new(aoc.animationClips[0], a);

            aoc.ApplyOverrides(o);

            animator.runtimeAnimatorController = aoc;
        }

        public void Die(bool byPlayer){
            if(!enabled) animator.SetTrigger("die");

            if(byPlayer) GameManager.Current.PlaySound(deathSound);

            IsDead = true;

            StartCoroutine(Appear(false));
        }

        private IEnumerator Appear(bool value){
            float start = value ? 0 : 1, end = value ? 1 : 0;

            if(!value && !enabled) yield return new WaitForSeconds(1);

            for(float animation = start; animation != end; animation = Mathf.MoveTowards(animation, end, Time.deltaTime * 2)){
                transform.localScale = Vector3.one * Animations.EaseOutBack(animation, 2);

                yield return null;
            }

            if(!value) gameObject.SetActive(false);
        }
    }
}