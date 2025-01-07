using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TD.Runtime.Tower;
using UnityEngine;
using static TD.Runtime.Tools.ScEnums;
using Random = UnityEngine.Random;

namespace TD.Runtime.Gacha {
    public class ScGacha : MonoBehaviour {
        public List<StLootTable> NormalPull;
        public List<StLootTable> LuckyPull;
        public List<SoTower> Towers;

        [SerializeField] private int _pullPrize;
        [SerializeField] private int _pullCount;

        ScGachaCardsManager _cardsManager;


        private void Start() {
            _cardsManager = GetComponent<ScGachaCardsManager>();
        }

        ScTowerManager _towerManager=>ScTowerManager.Instance;

        public void StartSoloPull() {
            if (!_cardsManager.CanPull) return;
            if (_towerManager.Money - _pullPrize < 0) return;

            _cardsManager.CanPull = false;
            _towerManager.Money -= _pullPrize;
            _pullCount++;
            ScGachaFX.Instance.ToggleLuckyFX(false);

            if (_cardsManager.IsAllCardShowed) {
                
                StartCoroutine(HideAndPullCards());
            }
            else {
                List<StTowerCard> towers = Pull();
                _cardsManager.SetCards(towers);
                _cardsManager.ShowCards();
            }
        }

        private IEnumerator HideAndPullCards() {
            yield return _cardsManager.HideCards();
            List<StTowerCard> towers = Pull();
            _cardsManager.SetCards(towers);
            _cardsManager.ShowCards();
        }
        
        List<StTowerCard> Pull() {
            List<StTowerCard> selectedTowers = new();
            
            bool isLucky = Random.Range(1, 101) < 3;
            List<StLootTable> pull = isLucky ? LuckyPull : NormalPull;
            ScGachaFX.Instance.ToggleLuckyFX(isLucky);
            
            
            for (int i = 0; i < 5; i++) {
                Rarity rarity = ChooseRarity(pull);
                
                SoTower tower = Towers[Random.Range(0, Towers.Count)];
                int fragments = GetFragments(rarity, pull);
                
                StTowerCard card = new() { Tower = tower, Rarity = rarity, Level = 1 , Fragments = fragments};
                selectedTowers.Add(card);
            }
            return selectedTowers;
        }

        Rarity ChooseRarity(List<StLootTable> lootTables) {
            int weight = lootTables.Sum(loot => loot.Probability);

            int random = Random.Range(0, weight);
            int cumulativeWeight = 0;
            
            foreach (StLootTable loot in lootTables) {
                cumulativeWeight += loot.Probability;
                if (random < cumulativeWeight) {
                    return loot.Rarity;
                }
            }

            return Rarity.Common;
        }

        int GetFragments(Rarity rarity, List<StLootTable> lootTables) {
            int fragments = lootTables.First(loot => loot.Rarity == rarity).Fragments;
            return fragments;
        }
    }
    
    [System.Serializable]
    public struct StLootTable{
        public Rarity Rarity;
        public int Probability;
        public int Fragments;
    }
    
}