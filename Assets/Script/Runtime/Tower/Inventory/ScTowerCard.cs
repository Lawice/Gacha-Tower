using TD.Runtime.GridSystem;
using TD.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TD.Runtime.Tools.ScEnums;

namespace TD.Runtime.Tower.Inventory {
    public class ScTowerCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        ScTowerManager _towerManager => ScTowerManager.Instance;
        ScGridManager _gridManager => ScGridManager.Instance;
        
        [SerializeField] private StTowerCard _tower;
        [SerializeField] private int _count;
        [SerializeField] private int _realCost;
        
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _rarityImage;
        [SerializeField] private Image _backgroundImage;

        public void SetTower(StTowerCard stTower, int count) {
            _tower = stTower;
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
            _rarityImage.sprite = Resources.Load<Sprite>("Sprites/Cards/Rarity/" + _tower.Rarity);
            _backgroundImage.sprite = Resources.Load<Sprite>("Sprites/Cards/Girls/" + _nameText.text);
        }

        public void OnClick() {
            if (_towerManager.Money < _realCost) {
                return;
            }

            StCardInventory cardInventory = new() { Tower = _tower.Tower, Rarity = _tower.Rarity };

            _towerManager.RemoveMoney(_realCost);
            if (_towerManager.Towers.TryGetValue(cardInventory, out StCardInventoryValue towerData)) {
                towerData.Count--;
                _towerManager.Towers[cardInventory] = towerData;
            }
            
            if (_gridManager.SelectedTile != null) {
                _gridManager.SelectedTile.PlaceTower(_tower);
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            _gridManager.SetPreview( _gridManager.SelectedTile.TilePosition, _tower.Tower.Range);
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