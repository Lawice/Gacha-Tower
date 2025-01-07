using System;
using TD.Runtime.InputSystem;
using TD.Player;
using UnityEngine;

using TD.Runtime.Tower;

namespace TD.Runtime.GridSystem {
    public class ScGridTile : MonoBehaviour {
        public Vector2Int TilePosition;
        public bool IsTowerPlaceable;
        public ITower TowerOnTile;

        public bool IsTowerPlaced;
        Color _startColor;
        private MeshRenderer _renderer;
        ScGridManager _gridManager => ScGridManager.Instance;
        
        private void Start() {
            TilePosition = new Vector2Int((int)(transform.localPosition.x), (int)(transform.localPosition.z));
            _renderer = GetComponentInChildren<MeshRenderer>();
            _startColor = _renderer.material.color;

        }

        private void Update() {
            if (ScInputManager.Instance.IsEscaping && _gridManager.SelectedTile == this) {
                CloseTowerSelection();
            }
        }

        public void OpenTowerSelection() {
            _renderer.material.color = Color.black;
            ScTowerManager.Instance.ShowCards();
        }
        
        public void CloseTowerSelection() {

            ScTowerManager.Instance.HideCards();
            _renderer.material.color = _startColor;
        }
        
        public void SetTower(ITower tower) {
            IsTowerPlaced = true;
            TowerOnTile = tower;
        }
        
        public void PlaceTower(StTowerCard stTower) {
            Debug.Log("Placing tower: " + stTower.Tower.Name);
            GameObject newTower = Instantiate(stTower.Tower.Prefab, new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z), Quaternion.identity);
            newTower.transform.parent = transform;
            newTower.transform.localScale = Vector3.one;
            ITower towerComp = newTower.GetComponent<ITower>();
            SetTower(towerComp);
            towerComp.InitTower(TilePosition, stTower.Rarity,stTower.Level, stTower.Tower.Range);
            CloseTowerSelection();
            _gridManager.ClearPreview();
        }
        
        
    }
}