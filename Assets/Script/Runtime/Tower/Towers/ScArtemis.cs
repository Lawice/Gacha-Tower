using TD.Runtime.Enemy;
using TD.Runtime.Tower.Projectiles;
using UnityEngine;

namespace TD.Runtime.Tower {
    public class ScArtemis : ScTower {
        [Header("Projectile")]
        [SerializeField] GameObject _projectilePrefab;
        [SerializeField] int _baseDamage;
        [SerializeField] float _projectileSpeed;

        private float _lastAttackTime;
        private void Update() {
            GetEnemiesInRange();
            if (_enemiesInRange.Count <= 0 || !(Time.time >= _lastAttackTime + AttackCooldown)) {
                return;
            }
            GameObject targetEnemy = null;
            float maxDistance = 0;
            foreach (Transform enemy in _enemiesInRange) {
                ScEnemyMovement enemyMovement = enemy.GetComponent<ScEnemyMovement>();
                if (enemyMovement == null || !(enemyMovement.TotalDistance > maxDistance)) {
                    continue;
                }
                maxDistance = enemyMovement.TotalDistance;
                targetEnemy = enemy.gameObject;
            }
            if (targetEnemy != null) {
                Attack(targetEnemy.transform.position);
            }
            _lastAttackTime = Time.time;
        }

        public override void Attack(Vector3 target) {
            GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<ScProjectile>().Init(target, _projectileSpeed, _baseDamage);
            
        }
    }
}