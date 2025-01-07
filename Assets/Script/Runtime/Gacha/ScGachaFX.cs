using System;
using System.Collections.Generic;
using UnityEngine;
using static TD.Runtime.Tools.ScEnums;

namespace TD.Runtime.Gacha {
    public class ScGachaFX : MonoBehaviour {
        public static ScGachaFX Instance;

        [SerializeField] private GameObject _heroicFX, _legendaryFX, _epicFX;

        [SerializeField] List<GameObject> _luckyFXs;
        
        private void Awake() {
            if(Instance == null) Instance = this;
        }

        private void Start() {
            foreach (GameObject fx in _luckyFXs) {
                fx.SetActive(false);
            }
        }

        public void ToggleLuckyFX(bool isLucky) {
            foreach (GameObject fx in _luckyFXs) {
                fx.SetActive(isLucky);
            }

            if (isLucky) {
                Debug.Log("LUCKY");
            }
        }
        
        
        public void PlayRarityFX(Rarity rarity, Vector3 position) {
            switch (rarity) {
                case Rarity.Heroic:
                    Instantiate(_heroicFX, position, Quaternion.identity);
                    break;
               case Rarity.Legendary:
                    Instantiate(_legendaryFX, position, Quaternion.identity);
                    break;
                case Rarity.Epic:
                    Instantiate(_epicFX, position, Quaternion.identity);
                    break;
                case Rarity.Rare:
                case Rarity.Common:
                    break;
            }
        }
    }
}