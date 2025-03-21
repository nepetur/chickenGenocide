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
            if(delay == 0){
                Spawn();
            }
            else{
                delay = Mathf.MoveTowards(delay, 0, Time.deltaTime);
            }
        }

        private void Spawn(){
            var chicken = GetOrCreate();

            if(chicken == null) return;

            delay = spawnDelay;

            chicken.Spawn();

            var range = bounds / 2;

            chicken.transform.localPosition = new Vector3(
                Random.Range(-range.x, range.x), 0, Random.Range(-range.z, range.z)
            );
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