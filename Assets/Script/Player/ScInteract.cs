using System;
using TD.GridSystem;
using TD.InputSystem;
using UnityEngine;

namespace TD.Player {
    public class ScInteract : MonoBehaviour {
        private ScGridManager _gridManager => ScGridManager.Instance;
        ScInputManager _inputManager => ScInputManager.Instance;
        
        private void Update() {
            if (_inputManager.IsInteracting) {
                TryInteractObject(_gridManager.GetCursorPosition());
            }
        }

        void TryInteractObject(Vector2Int position) {
            Debug.Log("Try placing tower at" + position);
            if(!_gridManager.TryGetTile(position, out ScGridTile tile)) return;
            if(!tile.IsTowerPlaceable) return;

            tile.OpenTowerSelection();
            
        }

        
        
    }
}