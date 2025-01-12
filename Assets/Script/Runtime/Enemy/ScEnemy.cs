using System;
using TD.Runtime.Tower.Inventory;
using UnityEngine;
using UnityEngine.Pool;

namespace TD.Runtime.Enemy {
    public class ScEnemy : MonoBehaviour {
        public SoEnemy Enemy;
        int _health;
        
        ObjectPool<GameObject> _pool;

        private void Start() {
            _health = Enemy.Health;
        }

        public void TakeDamage(int damage) {
            if (_health - damage <= 0) {
                ScTowerManager.Instance.AddMoney(Enemy.Reward);
                Destroy();
            }
            _health -= damage;
        }

        public void SetPool(ObjectPool<GameObject> pool) {
            _pool = pool;
        }

        public void Destroy() {
            _pool.Release(gameObject);
        }
    }
}