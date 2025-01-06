using UnityEngine;
using UnityEngine.UI;

using static TD.Runtime.Tools.ScTools;

namespace TD.Runtime.GridSystem {
    public class ScGridCursor : MonoBehaviour {
        public static ScGridCursor Instance;
        private Camera _camera;
        ScGridManager _gridManager => ScGridManager.Instance;
        private Image _cursorImage;

        [field:SerializeField]public Vector2Int CursorPosition { get; private set; }
        private Plane _gridPlane;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
        }

        private void Start() {
            transform.localScale = new Vector3(_gridManager.TileSize, _gridManager.TileSize, _gridManager.TileSize);
            _camera = Camera.main;
            _gridPlane = new Plane(Vector3.up, Vector3.zero);
            _cursorImage = GetComponent<Image>();

        }

        private void Update() {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f)) {
                ScGridTile tile = hit.collider.GetComponent<ScGridTile>();
                if (tile != null) {
                    CursorPosition = tile.TilePosition;
                    transform.position = new Vector3(CursorPosition.x+_gridManager.TileSize/2f, transform.position.y, CursorPosition.y+_gridManager.TileSize/2f);
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