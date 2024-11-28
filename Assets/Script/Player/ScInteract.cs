using TD.GridSystem;
using TD.InputSystem;
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
            if(!_gridManager.TryGetTile(position, out ScGridTile tile)) return;
            if(!tile.IsTowerPlaceable) return;

            _gridManager.SelectedTile = tile;
            if(!tile.IsTowerPlaced) tile.OpenTowerSelection();
            
            _inputManager.IsInteracting = false;
            
            
        }

        
        
    }
}