using UnityEngine;

namespace TD.Runtime.Enemy {
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy", order = 0)]
    public class SoEnemy : ScriptableObject {
        public string Name;
        public GameObject Prefab;
        public int Health;
        public int Reward;
        public int Damage;
        public float Speed;
        public float Resistance;
    }
}