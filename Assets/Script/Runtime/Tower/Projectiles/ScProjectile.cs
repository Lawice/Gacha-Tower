using TD.Runtime.Enemy;
using UnityEngine;

namespace TD.Runtime.Tower.Projectiles {
    public class ScProjectile: MonoBehaviour {
        private Vector3 _target;
        private float _speed;
        [SerializeField]LayerMask _enemyLayer;
        int _damage;

        public void Init(Vector3 target, float speed, int damage) {
            _target = target;
            _speed = speed;
            _damage = damage;
            transform.LookAt(_target);
        }
        
        private void Update() {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.TryGetComponent(out ScEnemy enemy)) {
                enemy.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}