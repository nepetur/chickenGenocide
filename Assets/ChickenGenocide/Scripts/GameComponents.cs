using UnityEngine;

namespace ChickenGenocide{
    public class GameComponents : MonoBehaviour{
        [Space, SerializeField] private AudioSource audioSource;
        
        public AudioSource AudioSource => audioSource;
    }
}