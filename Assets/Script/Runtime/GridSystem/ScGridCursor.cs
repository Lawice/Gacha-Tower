using TD.Runtime.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace TD.Runtime.GridSystem {
    public class ScGridCursor : MonoBehaviour {
        [SerializeField]private Camera _camera;
        private ScGridManager _gridManager => ScGridManager.Instance;
        private Image _cursorImage;

        [field: SerializeField] public Vector2Int CursorPosition;
        private Plane _gridPlane;
        

        private void Start() {
            transform.localScale = new Vector3(_gridManager.TileSize, _gridManager.TileSize, _gridManager.TileSize);
            _gridPlane = new Plane(Vector3.up,  new Vector3(0, 0.75f, 0));
            _cursorImage = GetComponent<Image>();
        }

        private void Update() {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);

            if (_gridPlane.Raycast(ray, out float distance)) {
                Debug.Log($"Hit Distance: {distance}");
                Vector3 hit = ray.GetPoint(distance);
                Debug.Log("hit :" + hit);

                CursorPosition = new() {
                    x = Mathf.FloorToInt(hit.x / _gridManager.TileSize),
                    y = Mathf.FloorToInt(hit.z / _gridManager.TileSize),
                };
                if (!_gridManager.OutOfBounds(CursorPosition)) {
                    transform.position = new Vector3( CursorPosition.x + _gridManager.TileSize / 2f,  transform.position.y, CursorPosition.y + _gridManager.TileSize / 2f);
                }

                _cursorImage.enabled = !_gridManager.OutOfBounds(CursorPosition);
            }
        }
    }
}
