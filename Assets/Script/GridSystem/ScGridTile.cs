using TD.InputSystem;
using TD.Player;
using UnityEngine;
using TD.Tower;

namespace TD.GridSystem {
    public class ScGridTile : MonoBehaviour {
        public Vector2Int TilePosition;
        public bool IsTowerPlaceable;
        public ITower TowerOnTile;
        public bool IsTowerPlaced;
        Color _startColor;
        private MeshRenderer _renderer;
        
        private void Start() {
            TilePosition = new Vector2Int((int)(transform.localPosition.x), (int)(transform.localPosition.z));
            _renderer = GetComponentInChildren<MeshRenderer>();
            _startColor = _renderer.material.color;

        }

        public void OpenTowerSelection() {
            ScGridManager.Instance.ToggleCursorLock(false);
            _renderer.material.color = Color.black;
            ScTowerInventory.Instance.ShowCards();
        }
        
        public void CloseTowerSelection() {
            ScGridManager.Instance.ToggleCursorLock(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ScTowerInventory.Instance.HideCards();
            _renderer.material.color = _startColor;
        }
        
        public void SetTower(ITower tower) {
            IsTowerPlaced = true;
            TowerOnTile = tower;
        }
        
        public void PlaceTower(TowerItem tower) {
            Debug.Log("Placing tower: " + tower.Tower.Name);
            GameObject newTower = Instantiate(tower.Tower.Prefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            newTower.transform.parent = transform;
            newTower.transform.localScale = Vector3.one;
            ITower towerComponent = newTower.GetComponent<ITower>();
            SetTower(towerComponent);
            towerComponent.SetLevel(tower.Level);
            towerComponent.SetRarity(tower.Rarity);
            towerComponent.SetPosition(TilePosition);
            CloseTowerSelection();
        }
        
        
    }
}