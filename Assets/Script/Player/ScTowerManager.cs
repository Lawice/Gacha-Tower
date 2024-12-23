using System;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using TD.GridSystem;
using TD.InputSystem;
using static TD.Tools.ScEnums;
using TD.Tower;
using TMPro;

namespace TD.Player {
    public class ScTowerManager : MonoBehaviour {
        public static ScTowerManager Instance { get; private set; }
        public SerializedDictionary<TowerItem, int> Towers = new();

        ScGridManager _gridManager => ScGridManager.Instance;
        
        public int Money = 300;
        
        public GameObject CardPrefab;
        public Transform CardParent;
        public GameObject CardContainer;
        
        [SerializeField]private TextMeshProUGUI _moneyText;
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(transform.root);
            }
            else {
                Destroy(this);
            }
        }

        private void Start() {
            _moneyText.text = "Money : " + Money;
            CardContainer.SetActive(false);
        }

        public void ShowCards() {
            _moneyText.text = "Money : " + Money;
            _gridManager.ToggleCursorLock(false);
            if (Towers.Count == 0) {
                _gridManager.SelectedTile.CloseTowerSelection();
                return;
            }
            CardContainer.SetActive(true);
            UpdateCards();
        }
        
        public void HideCards() {
            _gridManager.ToggleCursorLock(true);
            _moneyText.text = "Money : " + Money;
            CardContainer.SetActive(false);
        }
        
        public void AddTower(SoTower tower, Rarity rarity, int level) {
            Towers.Add(new TowerItem { Tower = tower, Rarity = rarity, Level = level }, 1);
        }

        public void UpdateCards() {
            DestroyCards();
            AddCards();
        }

        private void AddCards() {
            foreach (var tower in Towers) {
                if (tower.Value > 0) {
                    var card = Instantiate(CardPrefab, CardParent);
                    ScTowerCard cardComponent = card.GetComponent<ScTowerCard>();
                    cardComponent.SetTower(tower.Key, tower.Value);
                }
            }
        }

        private void DestroyCards() {
            foreach (Transform child in CardParent) {
                Destroy(child.gameObject);
            }
        }
    }
}