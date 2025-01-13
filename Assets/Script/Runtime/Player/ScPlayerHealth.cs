using TMPro;
using UnityEngine;

namespace TD.Runtime.Player {
    public class ScPlayerHealth : MonoBehaviour {
        public int Health = 250;
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] TextMeshProUGUI _healthText;
        
        public static ScPlayerHealth Instance { get; private set; }
        
        private void Awake() {
            if( Instance == null ) Instance = this;
            else Destroy(this);
        }

        void Start() {
            _healthText.text = "♥ : "+Health;
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