using System;
using TD.Runtime.Tower;
using UnityEngine;
using UnityEngine.UI;

using static TD.Runtime.Tools.ScTools;

namespace TD.Runtime.GridSystem {
    public class ScGridCursor : MonoBehaviour {
        [SerializeField]private Camera _camera;
        private ScGridManager _gridManager => ScGridManager.Instance;
        private Image _cursorImage;

        [field: SerializeField] public Vector2Int CursorPosition;
        private Plane _gridPlane;
        

        private void Start() {
            transform.localScale = new Vector3(_gridManager.TileSize, _gridManager.TileSize, _gridManager.TileSize);
            _gridPlane = new Plane(Vector3.up, Vector3.zero);
            _cursorImage = GetComponent<Image>();
        }

        private void Update() {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f)) {
                ScGridTile tile = hit.collider.GetComponent<ScGridTile>();
                ScTower tower = hit.collider.GetComponent<ScTower>();

                if (tile != null) {
                    CursorPosition = tile.TilePosition;
                    transform.position = new Vector3(
                        CursorPosition.x + _gridManager.TileSize / 2f,
                        transform.position.y,
                        CursorPosition.y + _gridManager.TileSize / 2f
                    );
                } else if (tower != null) {
                    _gridManager.SelectedTower = tower;
                    CursorPosition = new Vector2Int(-1, -1);
                } else {
                    CursorPosition = new Vector2Int(-1, -1);
                }
            } else {
                CursorPosition = new Vector2Int(-1, -1);
            }
            
            _cursorImage.enabled = !_gridManager.OutOfBounds(CursorPosition);
        }
        
    }
}
