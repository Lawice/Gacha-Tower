using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace TD.Runtime.Player {
    public class ScPlayerHealth : MonoBehaviour {
        public int Health = 100;
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] TextMeshProUGUI _healthText;
        
        public static ScPlayerHealth Instance { get; private set; }
        
        private void Awake() {
            if( Instance == null ) Instance = this;
            else Destroy(this);
        }
        
        public void TakeDamage(int damage) {
            Health -= damage;
            _healthText.text = "♥ : "+Health;
            if(Health <= 0) {
                _gameOverPanel.SetActive(true);
            }
        }
    }
}