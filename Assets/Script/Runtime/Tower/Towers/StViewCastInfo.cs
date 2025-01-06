using UnityEngine;

namespace TD.Runtime.Tower {
    public struct StViewCastInfo {
        public bool Hit;
        public Vector3 Point;
        public float Distance;
        public float Angle;
        
        public StViewCastInfo(bool hit, Vector3 point, float distance, float angle) {
            Hit = hit;
            Point = point;
            Distance = distance;
            Angle = angle;
        }
    }
}