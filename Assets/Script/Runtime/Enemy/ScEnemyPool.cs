using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Runtime.Enemy {
    public class ScEnemyPool : MonoBehaviour{
        private ObjectPool<GameObject> _pool;
        [SerializeField] GameObject _enemyPrefab;

        private void Awake() {
            _pool = new ObjectPool<GameObject>(Create, OnTakeFromPool, OnReturnedToPool);
        }

        private void OnReturnedToPool(GameObject obj) {
            obj.SetActive(false);
        }

        private void Update() {
            GameObject newEnemy = _pool.Get();
            newEnemy.transform.position = transform.position;
        }

        private void OnTakeFromPool(GameObject obj) {
            obj.SetActive(true);
        }

        private GameObject Create() {
            GameObject enemy = Instantiate(_enemyPrefab);
            return enemy;
        }
    }
}