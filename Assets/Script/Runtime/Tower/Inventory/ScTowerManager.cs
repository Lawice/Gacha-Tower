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

        public void UpdateCards() {
            DestroyCards();
            AddCards();
        }

        private void AddCards() {
            foreach (KeyValuePair<StCardInventory, StCardInventoryValue> tower in Towers) {
                if (tower.Value.Count <= 0) continue;
                GameObject card = Instantiate(CardPrefab, CardParent);
                ScTowerCard cardComponent = card.GetComponent<ScTowerCard>();
                cardComponent.SetTower(new StTowerCard{ Tower = tower.Key.Tower, Rarity = tower.Key.Rarity }, tower.Value.Count);
            }
        }

        private void DestroyCards() {
            foreach (Transform child in CardParent) {
                Destroy(child.gameObject);
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