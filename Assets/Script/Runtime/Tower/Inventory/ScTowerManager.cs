using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using TD.Runtime.GridSystem;
using TD.Runtime.Tower;
using static TD.Runtime.Tools.ScEnums;
using TMPro;
using TD.Runtime.Tower.Inventory;
using UnityEngine.Serialization;

namespace TD.Runtime.Tower {
    public class ScTowerManager : MonoBehaviour {
        public static ScTowerManager Instance { get; private set; }
        public SerializedDictionary<StCardInventory, StCardInventoryValue> Towers = new();
        private Dictionary<StCardInventory, GameObject> _activeCards = new ();

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
            UpdateMoneyText();
            CardContainer.SetActive(false);
        }
        
        public void AddMoney(int amount) {
            Money += amount;
            UpdateMoneyText();
        }
        
        public void RemoveMoney(int amount) {
            Money -= amount;
            UpdateMoneyText();
        }

        void UpdateMoneyText() {
            _moneyText.text = "Money : " + Money;
        }

        public void ShowCards() {
            UpdateMoneyText();
            _gridManager.ToggleCursorLock(false);
            if (Towers.Count == 0 || Towers.All(tower => tower.Value.Count <= 0)) {
                _gridManager.SelectedTile.CloseTowerSelection();
                return;
            }
            CardContainer.SetActive(true);
            UpdateCards();
        }
        
        public void HideCards() {
            _gridManager.ToggleCursorLock(true);
            UpdateMoneyText();
            CardContainer.SetActive(false);
        }
        
        public void AddCard(SoTower tower, Rarity rarity, int fragments) {
            if (tower == null)return;

            StCardInventory key = new() { Tower = tower, Rarity = rarity };

            if (Towers.TryGetValue(key, out var existingCardValue)) {
                Debug.Log($"Adding {fragments} fragments to {tower.Name}");
                existingCardValue.Fragments += fragments;
                existingCardValue.Count++;
        

                Towers[key] = existingCardValue;
        
                Debug.Log(existingCardValue.Fragments);
            } else {

                Towers.Add(key, new StCardInventoryValue { Fragments = fragments, Count = 1});
            }
        }
        void UpdateCards() {
            foreach (KeyValuePair<StCardInventory, StCardInventoryValue> tower in Towers) {
                if (tower.Value.Count > 0) {
                    // If the card already exists, update it
                    if (_activeCards.TryGetValue(tower.Key, out GameObject existingCard)) {
                        ScTowerCard cardComponent = existingCard.GetComponentInChildren<ScTowerCard>();
                        cardComponent.SetTower(new StTowerCard { Tower = tower.Key.Tower, Rarity = tower.Key.Rarity }, tower.Value.Count);
                        existingCard.SetActive(true); // Ensure it's visible
                    } 
                    // If the card does not exist, create it
                    else {
                        GameObject newCard = Instantiate(CardPrefab, CardParent);
                        ScTowerCard cardComponent = newCard.GetComponentInChildren<ScTowerCard>();
                        cardComponent.SetTower(new StTowerCard { Tower = tower.Key.Tower, Rarity = tower.Key.Rarity }, tower.Value.Count);
                        _activeCards[tower.Key] = newCard;
                    }
                } else {
                    // If the card exists and count is 0, hide it
                    if (_activeCards.TryGetValue(tower.Key, out GameObject existingCard)) {
                        existingCard.SetActive(false); // Hide the card instead of destroying
                    }
                }
            }
        }
    }
}
[System.Serializable]
public struct StCardInventory {
    public SoTower Tower;
    public Rarity Rarity;
}
[System.Serializable]
public struct StCardInventoryValue {
    public int Count;
    public int Fragments;
}