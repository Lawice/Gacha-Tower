using TD.GridSystem;
using TD.Player;
using TMPro;
using UnityEngine;
using static TD.Tools.ScEnums;

namespace TD.Tower {
    public class ScTowerCard : MonoBehaviour {
        ScTowerInventory _towerInventory => ScTowerInventory.Instance;
        ScGridManager _gridManager => ScGridManager.Instance;
        
        [SerializeField] private TowerItem Tower;
        [SerializeField] private int Count;
        
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _nameText;

        public void SetTower(TowerItem tower, int count) {
            Tower = tower;
            Count = count;
            _priceText.text = Tower.Tower.Cost.ToString();
            _nameText.text = Tower.Tower.Name;
        }

        public void OnClick() {
            if(_towerInventory.Money - Tower.Tower.Cost < 0) return;

            _towerInventory.Money -= Tower.Tower.Cost;
            if (_towerInventory.Towers[Tower] - 1 > 0) {
                _towerInventory.Towers[Tower]--;
            }
            else {
                _towerInventory.Towers.Remove(Tower);
            }
            
            _gridManager.SelectedTile.PlaceTower(Tower);
        }

    }
    
    [System.Serializable]
    public struct TowerItem {
        public SoTower Tower;
        public Rarity Rarity;
        public int Level;
    }
}