using TD.Runtime.GridSystem;

using TD.Runtime.InputSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace TD.Player {
    public class ScInteract : MonoBehaviour {
        private ScGridManager _gridManager => ScGridManager.Instance;
        ScInputManager _inputManager => ScInputManager.Instance;
        
        private void Update() {
            if (_inputManager.IsInteracting && !_inputManager.IsCameraLocked) {
                TryInteractObject(_gridManager.GetCursorPosition());
            }
        }

        void TryInteractObject(Vector2Int position) {
            Debug.Log(position);
            HideTowerUpgrade();
            if (!_gridManager.TryGetTile(position, out ScGridTile tile)) {
                if (_gridManager.SelectedTower != null) {
                    if (_gridManager.TryGetTowerTile()) {
                        _gridManager.SelectedTile.OpenTowerUpgrade();
                    }
                }
                return;
            }
            if (!tile.IsTowerPlaceable) {
                return;
            }

            _gridManager.SelectedTile = tile;
            if (!tile.IsTowerPlaced) {
                tile.OpenTowerSelection();
            }
            
            _inputManager.IsInteracting = false;
        }

        void HideTowerUpgrade() {
            if (_gridManager.SelectedTile == null) return;
            if(!_gridManager.SelectedTile.IsTowerPlaced)return;
            _gridManager.SelectedTile.CloseTowerUpgrade();
        }


        
        
    }
}