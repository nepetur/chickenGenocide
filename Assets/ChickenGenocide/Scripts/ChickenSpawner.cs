using UnityEngine;
using System.Collections.Generic;

namespace ChickenGenocide{
    public class ChickenSpawner : MonoBehaviour{
        [Space, SerializeField] private Chicken prefab;

        [Space, SerializeField] private int maxCount;

        [Space, SerializeField] private float spawnDelay;

        [Space, SerializeField] private Vector3 bounds;

        private float delay;
        private List<Chicken> chickens = new();

        private void Update(){
            foreach(var c in chickens){
                if(c.enabled == false || c.IsDead) continue;

                var x = Mathf.Abs(c.transform.localPosition.x);

                if(x > bounds.x / 2) c.Die(false);
            }

            if(delay == 0){
                Spawn();
            }
            else{
                delay = Mathf.MoveTowards(delay, 0, Time.deltaTime);
            }
        }

        private void Spawn(){
            delay = spawnDelay;

            var chicken = GetOrCreate();

            if(chicken == null) return;

            var flying = Random.Range(0, 3) == 0;

            chicken.enabled = flying;

            var range = bounds / 2;

            if(flying){
                var direction = Random.Range(0, 2) == 0 ? 1 : -1;

                chicken.transform.localEulerAngles = Vector3.up * 90 * direction;

                chicken.transform.localPosition = new Vector3(
                    range.x * direction, Random.Range(range.y, bounds.y), Random.Range(-range.z, range.z)
                );
            }
            else{
                chicken.transform.localPosition = new Vector3(
                    Random.Range(-range.x, range.x), 0, Random.Range(-range.z, range.z)
                );

                chicken.transform.localEulerAngles = Vector3.zero;
            }

            chicken.Spawn();
        }

        private Chicken GetOrCreate(){
            foreach(var c in chickens){
                if(c.gameObject.activeSelf == false) return c;
            }

            if(chickens.Count == maxCount) return null;

            var chicken = Instantiate(prefab, transform);

            chickens.Add(chicken);

            return chicken;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() => Gizmos.DrawWireCube(Vector3.up * bounds.y / 2, bounds);
        #endif
    }
}