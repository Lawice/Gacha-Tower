using UnityEngine;
using UnityEngine.UI;
using static TD.Tools.ScTools;

namespace TD.GridSystem {
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
            if (_gridPlane.Raycast(ray, out float hit)) {
                Vector3 hitPoint = ray.GetPoint(hit);
                hitPoint.x = RoundToNearest(hitPoint.x, _gridManager.TileSize);
                hitPoint.z = RoundToNearest(hitPoint.z, _gridManager.TileSize);
                transform.position = new Vector3(hitPoint.x +_gridManager.TileSize/2f, hitPoint.y +_gridManager.TileSize/2f +0.05f, hitPoint.z +_gridManager.TileSize/2f);
                CursorPosition = new Vector2Int((int)hitPoint.x/_gridManager.TileSize, (int)hitPoint.z/_gridManager.TileSize);
            }

            _cursorImage.enabled = !_gridManager.OutOfBounds(CursorPosition);
        }
    }
}