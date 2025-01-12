using System;
using TD.Runtime.Tools;
using TD.Runtime.Tower.Projectiles;
using UnityEngine;

namespace TD.Runtime.Tower {
    public class ScArtemis : ScTower {
        [SerializeField] public GameObject projectilePrefab;

        private float _lastAttackTime;
        private void Update() {
            GetEnemiesInRange();
            if (_enemiesInRange.Count > 0 && Time.time >= _lastAttackTime + AttackCooldown) {
                Attack(_enemiesInRange[0].transform.position);
                _lastAttackTime = Time.time;
            }
        }

        public override void Attack(Vector3 target) {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<ScArrows>().Init(target, 10f, 3);
            
        }
    }
}