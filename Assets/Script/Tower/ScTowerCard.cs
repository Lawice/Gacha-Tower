using TD.GridSystem;
using TD.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static TD.Tools.ScEnums;

namespace TD.Tower {
    public class ScTowerCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        ScTowerInventory _towerInventory => ScTowerInventory.Instance;
        ScGridManager _gridManager => ScGridManager.Instance;
        
        [SerializeField] private TowerItem Tower;
        [SerializeField] private int Count;
        [SerializeField] private int RealCost;
        
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _starsText;

        public void SetTower(TowerItem tower, int count) {
            Tower = tower;
            Count = count;

            switch (Tower.Rarity) {
                case Rarity.Common:
                    RealCost = Tower.Tower.BaseCost;
                    break;
                case Rarity.Rare:
                    RealCost =  (int)(Tower.Tower.BaseCost * 1.1f);
                    break;
                case Rarity.Epic:
                    RealCost = (int)(Tower.Tower.BaseCost * 1.25f);
                    break;
                case Rarity.Legendary:
                    RealCost = (int)(Tower.Tower.BaseCost * 1.35f); 
                    break;
                case Rarity.Heroic:
                    RealCost = (int)(Tower.Tower.BaseCost * 1.5f);
                    break;
            }
            
            _priceText.text = RealCost.ToString();
            _nameText.text = Tower.Tower.Name;
            _starsText.text = Tower.Rarity.ToString();
        }

        public void OnClick() {
            if(_towerInventory.Money - RealCost < 0) return;

            _towerInventory.Money -= RealCost;
            if (_towerInventory.Towers[Tower] - 1 > 0) {
                _towerInventory.Towers[Tower]--;
            }
            else {
                _towerInventory.Towers.Remove(Tower);
            }
            
            _gridManager.SelectedTile.PlaceTower(Tower);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            Debug.Log("AAAA");
            _gridManager.SetPreview( _gridManager.SelectedTile.TilePosition, Tower.Tower.Range / 10);
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