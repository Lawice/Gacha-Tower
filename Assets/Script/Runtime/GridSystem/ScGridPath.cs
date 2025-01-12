using System;
using TD.Runtime.Enemy;
using TD.Runtime.Player;
using UnityEngine;

namespace TD.Runtime.GridSystem {
    public class ScGridPath : MonoBehaviour {
        public bool IsStart;
        public bool IsEnd;
        
        public int PathNumber;

        private void OnTriggerEnter(Collider other) {
            if (IsEnd) {
                if(other.gameObject.TryGetComponent(out ScEnemy enemy)) {
                    ScPlayerHealth.Instance.TakeDamage(enemy.Enemy.Damage);
                    enemy.Destroy();
                }
            }
        }
    }
}

