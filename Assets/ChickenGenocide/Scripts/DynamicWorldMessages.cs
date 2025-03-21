using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace ChickenGenocide{
    public class DynamicWorldMessages : MonoBehaviour{
        public static DynamicWorldMessages Current {get; private set;}

        [Space, SerializeField] private TextMeshPro messagePrefab;

        private List<Message> messages = new();

        const float minLifeTime = 0, maxTimeLife = 1;

        private const float scaleAnimationPopUpDelta = 2f;

        private Vector3 randomPosition{
            get{
                const float radius = .25f;
                
                return new Vector3(
                    Random.Range(-radius, radius), Random.Range(-radius, radius)
                );
            }
        }

        private class Message{
            public TextMeshPro content;

            public float lifeTime;
        }

        private void Awake(){
            Current = this;
        }

        private void Update(){
            foreach(var m in messages){
                if(!m.content.gameObject.activeSelf) continue;

                m.lifeTime = Mathf.MoveTowards(m.lifeTime, minLifeTime, Time.deltaTime);

                m.content.transform.position += Vector3.up * Time.deltaTime;

                m.content.alpha = 2 * m.lifeTime - Mathf.Pow(m.lifeTime, 2);

                var scale = 1 + (scaleAnimationPopUpDelta + 1) * Mathf.Pow(-m.lifeTime, 3) + scaleAnimationPopUpDelta * Mathf.Pow(m.lifeTime, 2);

                m.content.transform.localScale = Vector3.one * scale;

                if(m.lifeTime == minLifeTime) m.content.gameObject.SetActive(false);
            }
        }

        private Message GetOrCreate(){
            foreach(var m in messages){
                if(!m.content.gameObject.activeSelf) return m;
            }

            var created = new Message();
            created.content = Instantiate(messagePrefab, transform);

            messages.Add(created);

            return created;
        }
        public void ShowMessage(string message, Vector3 position){
            var m = GetOrCreate();

            m.content.gameObject.SetActive(true);

            m.lifeTime = maxTimeLife;

            m.content.transform.position = position + randomPosition;

            m.content.transform.localScale = Vector3.zero;

            m.content.text = message;
        }
    }
}