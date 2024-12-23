using TD.GridSystem;
using TD.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using static TD.Tools.ScEnums;

namespace TD.Tower {
    public class ScTowerCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        ScTowerManager _towerManager => ScTowerManager.Instance;
        ScGridManager _gridManager => ScGridManager.Instance;
        
        [SerializeField] private TowerItem _tower;
        [SerializeField] private int _count;
        [SerializeField] private int _realCost;
        
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _starsText;

        public void SetTower(TowerItem tower, int count) {
            _tower = tower;
            _count = count;

            switch (_tower.Rarity) {
                case Rarity.Common:
                    _realCost = _tower.Tower.BaseCost;
                    break;
                case Rarity.Rare:
                    _realCost =  (int)(_tower.Tower.BaseCost * 1.1f);
                    break;
                case Rarity.Epic:
                    _realCost = (int)(_tower.Tower.BaseCost * 1.25f);
                    break;
                case Rarity.Legendary:
                    _realCost = (int)(_tower.Tower.BaseCost * 1.35f); 
                    break;
                case Rarity.Heroic:
                    _realCost = (int)(_tower.Tower.BaseCost * 1.5f);
                    break;
            }
            
            _priceText.text = _realCost.ToString();
            _nameText.text = _tower.Tower.Name;
            _starsText.text = _tower.Rarity.ToString();
        }

        public void OnClick() {
            if(_towerManager.Money - _realCost < 0) return;

            _towerManager.Money -= _realCost;
            if (_towerManager.Towers[_tower] - 1 > 0) {
                _towerManager.Towers[_tower]--;
            }
            else {
                _towerManager.Towers.Remove(_tower);
            }
            
            _gridManager.SelectedTile.PlaceTower(_tower);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            Debug.Log("AAAA");
            _gridManager.SetPreview( _gridManager.SelectedTile.TilePosition, _tower.Tower.Range / 10);
        }

        public void OnPointerExit(PointerEventData eventData) {
            _gridManager.ClearPreview();
        }
    }
    
    [System.Serializable]
    public struct TowerItem {
        public SoTower Tower;
        public Rarity Rarity;
        public int Level;
    }
}