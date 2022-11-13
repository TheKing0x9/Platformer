using UnityEngine;

namespace Platformer.Extensions {
    public static class Vector2Extensions {
        public static Vector2 Rotate(this Vector2 v, float degrees) {
            float rads = degrees * Mathf.Deg2Rad;

            float cos = Mathf.Cos(rads);
            float sin = Mathf.Sin(rads);

            return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
        }
    }
}