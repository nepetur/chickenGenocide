using UnityEngine;

namespace ChickenGenocide{
    public static class Animations{
        public static float EaseOutBack(float time, float delta){
            return 1 + (delta + 1) * Mathf.Pow(time - 1, 3) + delta * Mathf.Pow(time - 1, 2);
        }

        public static float SineInterpolation(float time){
            return Mathf.Sin(1.5f * Mathf.PI * time);
        }
    }
}