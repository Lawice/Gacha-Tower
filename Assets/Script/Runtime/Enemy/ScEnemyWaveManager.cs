using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Runtime.Enemy {
    public class ScEnemyWaveManager : MonoBehaviour {
        List<GameObject> _enemies = new();
        
        List<List<WaveElements>> _waves = new();

        [SerializeField] List<WaveElements> _wave1;
        [SerializeField] List<WaveElements> _wave2;
        [SerializeField] List<WaveElements> _wave3;
        [SerializeField] List<WaveElements> _wave4;
        [SerializeField] List<WaveElements> _wave5;
        
        int _waveIndex;
        
        private ScEnemyPoolManager _poolManager;
        private bool _isSpawning;
        private bool _isWaveOver => _enemies.Count == 0 || _enemies.TrueForAll(enemy => !enemy.activeSelf);

        void Start() {
            _poolManager = GetComponent<ScEnemyPoolManager>();
            _waves.AddRange(new[] { _wave1, _wave2, _wave3, _wave4, _wave5 });
        }

        void Update() {
            if (_isSpawning || !_isWaveOver) {
                return;
            }
            _enemies.Clear();
            if (_waveIndex < _waves.Count) {
                StartCoroutine(SpawnWave(_waves[_waveIndex++]));
            }
        }


        IEnumerator SpawnWave(List<WaveElements> wave) {
            _isSpawning = true;
            foreach (WaveElements waveElement in wave) {
                for (int i = 0; i < waveElement.Count; i++) {
                    _enemies.Add(_poolManager.GetPool(waveElement.Enemy).Pool.Get());
                    yield return new WaitForSeconds(waveElement.DelayBetweenSpawns);
                }
            }
            _isSpawning = false;
        }
    }

    [System.Serializable]
    public struct WaveElements {
        public SoEnemy Enemy;
        public int Count;
        public float DelayBetweenSpawns;
    }
}