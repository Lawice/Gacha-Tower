using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TD.Runtime.Tower;
using TD.Runtime.Tower.Inventory;
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

        private bool _canPull = true;

        private void Start() {
            _cardsManager = GetComponent<ScGachaCardsManager>();
        }

        ScTowerManager _towerManager=>ScTowerManager.Instance;

        public void StartSoloPull() {
            if (!_cardsManager.CanPull||!_canPull) return;
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
        
        public void StartMultiPull() {
            if (!_cardsManager.CanPull||!_canPull) return;
            if (_towerManager.Money - _pullPrize * 10 < 0) return;

            _canPull = false;
            _towerManager.Money -= _pullPrize * 10;
            _pullCount += 10;

            ScGachaFX.Instance.ToggleLuckyFX(false);

            List<StTowerCard> allTowers = new();

            for (int i = 0; i < 10; i++) {
                List<StTowerCard> towers = Pull();
                allTowers.AddRange(towers);
            }

            StartCoroutine(DisplayMultiPullCards(allTowers));
        }

        private IEnumerator DisplayMultiPullCards(List<StTowerCard> allTowers) {
            for (int i = 0; i < allTowers.Count; i += 5) {
                if (_cardsManager.IsAllCardShowed) {
                    yield return _cardsManager.HideCards();
                }
                List<StTowerCard> currentBatch = allTowers.Skip(i).Take(5).ToList();
                
                _cardsManager.SetCards(currentBatch);
                _cardsManager.ShowCards();
                
                yield return new WaitUntil(() => _cardsManager.IsAllCardShowed);
                yield return new WaitForSeconds(1.6f);
                
                yield return _cardsManager.HideCards();
            }
            
            _canPull = true;
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