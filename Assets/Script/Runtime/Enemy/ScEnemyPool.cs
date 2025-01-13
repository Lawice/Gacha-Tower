using UnityEngine;
using UnityEngine.Pool;

namespace TD.Runtime.Enemy {
    public class ScEnemyPool : MonoBehaviour{
        public ObjectPool<GameObject> Pool;
        [SerializeField] private GameObject _enemyPrefab;
        
        private void Awake() {
            Pool = new ObjectPool<GameObject>(CreateObject, OnTakeFromPool, OnReturnedToPool, OnDestroyObject,true, 100);
        }

        private GameObject CreateObject() {
            GameObject enemy = Instantiate(_enemyPrefab, transform);
            enemy.GetComponent<ScEnemy>().SetPool(Pool);
            return enemy;
        }
        
        private void OnTakeFromPool(GameObject obj) {
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
        
        private void OnReturnedToPool(GameObject obj) {
            obj.SetActive(false);
        }
        
        private void OnDestroyObject(GameObject obj) {
            Destroy(obj);
        }

    }
}