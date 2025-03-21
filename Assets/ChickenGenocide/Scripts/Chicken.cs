using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace ChickenGenocide{
    public class Chicken : MonoBehaviour{
        private static AnimationClip[] animations;

        private static AudioClip deathSound;
        
        public bool IsDead {get; private set;}

        private const float scaleAnimationPopUpDelta = 2f;

        [Space, SerializeField] private Animator animator;

        [
            RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)
        ]
        private static void Init(){
            deathSound = Resources.Load<AudioClip>("DeathSound");

            animations = Resources.LoadAll<AnimationClip>("ChickenAnimations");
        }

        private void OnEnable(){
            var aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);

            var o = new KeyValuePair<AnimationClip, AnimationClip>[1];

            var newAnim = animations[ Random.Range(0, animations.Length) ];

            o[0] = new(aoc.animationClips[0], newAnim);

            aoc.ApplyOverrides(o);

            animator.runtimeAnimatorController = aoc;
        }

        public void Spawn(){
            gameObject.SetActive(true);

            IsDead = false;

            StartCoroutine(Appear(true));
        }

        public void Die(){
            animator.SetTrigger("die");

            GameManager.Current.PlaySound(deathSound);

            IsDead = true;

            StartCoroutine(Appear(false));
        }

        private IEnumerator Appear(bool value){
            float start = value ? 0 : 1, end = value ? 1 : 0;

            if(!value) yield return new WaitForSeconds(1);

            for(float animation = start; animation != end; animation = Mathf.MoveTowards(animation, end, Time.deltaTime * 2)){
                var currentScale = 1 + (scaleAnimationPopUpDelta + 1) * Mathf.Pow(animation - 1, 3) + scaleAnimationPopUpDelta * Mathf.Pow(animation - 1, 2);

                transform.localScale = Vector3.one * currentScale;

                yield return null;
            }

            if(!value) gameObject.SetActive(false);
        }
    }
}