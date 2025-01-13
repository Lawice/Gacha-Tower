using TD.Runtime.Tower.Projectiles;
using UnityEngine;

namespace TD.Runtime.Tower {
    public class ScShino : ScTower {
        [Header("Projectile")]
        [SerializeField] GameObject _projectilePrefab;
        [SerializeField] int _baseDamage;
        [SerializeField] float _projectileSpeed;

        private float _lastAttackTime;
        
        private void Update() {
            GetEnemiesInRange();
            if (_enemiesInRange.Count > 0 && Time.time >= _lastAttackTime + AttackCooldown) {
                Attack(_enemiesInRange[0].transform.position);
                _lastAttackTime = Time.time;
            }
        }

        public override void Attack(Vector3 target) {
            GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<ScProjectile>().Init(target, _projectileSpeed, _baseDamage);
            
        }
    }
}