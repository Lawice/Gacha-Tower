using TD.Runtime.GridSystem;
using TD.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using static TD.Runtime.Tools.ScEnums;

namespace TD.Runtime.Tower {
    public class ScTowerCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        ScTowerManager _towerManager => ScTowerManager.Instance;
        ScGridManager _gridManager => ScGridManager.Instance;
        
        [FormerlySerializedAs("_tower")] [SerializeField] private StTowerCard stTower;
        [SerializeField] private int _count;
        [SerializeField] private int _realCost;
        
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _starsText;

        public void SetTower(StTowerCard stTower, int count) {
            this.stTower = stTower;
            _count = count;

            switch (this.stTower.Rarity) {
                case Rarity.Common:
                    _realCost = this.stTower.Tower.BaseCost;
                    break;
                case Rarity.Rare:
                    _realCost =  (int)(this.stTower.Tower.BaseCost * 1.1f);
                    break;
                case Rarity.Epic:
                    _realCost = (int)(this.stTower.Tower.BaseCost * 1.25f);
                    break;
                case Rarity.Legendary:
                    _realCost = (int)(this.stTower.Tower.BaseCost * 1.35f); 
                    break;
                case Rarity.Heroic:
                    _realCost = (int)(this.stTower.Tower.BaseCost * 1.5f);
                    break;
            }
            
            _priceText.text = _realCost.ToString();
            _nameText.text = this.stTower.Tower.Name;
            _starsText.text = this.stTower.Rarity.ToString();
        }

        public void OnClick() {
            if(_towerManager.Money - _realCost < 0) return;

            _towerManager.Money -= _realCost;
            if (_towerManager.Towers[stTower] - 1 > 0) {
                _towerManager.Towers[stTower]--;
            }
            else {
                _towerManager.Towers.Remove(stTower);
            }
            
            _gridManager.SelectedTile.PlaceTower(stTower);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            Debug.Log("AAAA");
            _gridManager.SetPreview( _gridManager.SelectedTile.TilePosition, stTower.Tower.Range / 10);
        }

        public void OnPointerExit(PointerEventData eventData) {
            _gridManager.ClearPreview();
        }
    }
    
    [System.Serializable]
    public struct StTowerCard {
        public SoTower Tower;
        public Rarity Rarity;
        public int Level;
        public int Fragments;
    }
}