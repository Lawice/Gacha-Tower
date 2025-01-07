using System.Collections.Generic;
using TD.Runtime.InputSystem;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;


namespace TD.Runtime.GridSystem {
    public class ScGridManager : MonoBehaviour {
        public static ScGridManager Instance { get; private set; }
        
        [SerializeField] Transform _ground;
        public int TileSize = 1;
        ScGridTile[,] _grid;

        public Vector2Int GridSize;
        private ScGridCursor _cursor;
        public ScGridTile SelectedTile;
        
        [SerializeField] GameObject _previewTowerRange;
        private GameObject _previewTower;

        List<ScGridTile> _pathTiles = new(); //ScGridTile
        
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
        }

        private void Start() {
            StartGrid();
            _cursor = GetComponentInChildren<ScGridCursor>();
            
            AdjustScale();
        }

        private void AdjustScale() {
            _ground.localScale = new Vector3(TileSize, TileSize, TileSize);
            NavMeshSurface surface = _ground.GetComponentInChildren<NavMeshSurface>();
            surface.BuildNavMesh();
        }

        private void StartGrid() {
            ScGridTile[] tiles = _ground.GetComponentsInChildren<ScGridTile>();
            int maxX = 0, maxY = 0;
            foreach (ScGridTile tile in tiles) {
                maxX = Mathf.Max(maxX, tile.TilePosition.x);
                maxY = Mathf.Max(maxY, tile.TilePosition.y);
            }
            GridSize = new Vector2Int(maxX + 1, maxY + 1);
            Debug.Log("Grid Size: " + GridSize);
            _grid = new ScGridTile[GridSize.x, GridSize.y];
            foreach (ScGridTile tile in tiles) {
                _grid[tile.TilePosition.x, tile.TilePosition.y] = tile;
            }
        }
        
        public Vector2Int GetCursorPosition() => _cursor.CursorPosition;

        public bool TryGetTile(Vector2Int position, out ScGridTile tile) {
            ScGridTile gridTile = GetTile(position);
            if (gridTile == null) {
                tile = null;
                return false;
            }
            tile = gridTile;
            return true;
        }

        public ScGridTile GetTile(Vector2Int position) {
            return GetTile(position.x, position.y);
        }

        public ScGridTile GetTile(int x, int y) {

            if (OutOfBounds(new Vector2Int(x,y))) return null;
            return _grid[x, y];
        }

        public bool OutOfBounds(Vector2Int position) => position.x < 0 || position.y < 0 || position.x >= GridSize.x || position.y >= GridSize.y;

        public void ToggleCursorLock(bool lockCursor) {
            ScInputManager.Instance.IsCameraLocked = !lockCursor;
            Cursor.lockState = lockCursor? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCursor;
            _cursor.gameObject.SetActive(lockCursor);
        }
        
        public void SetPreview(Vector2Int position, int range) {
            if (_previewTower == null) {
                _previewTower = Instantiate(_previewTowerRange);
            }
            else {
                Debug.Log(_previewTower);
            }
            _previewTower.SetActive(true);
            _previewTower.transform.position = new Vector3(position.x + 0.5f, 0.5f, position.y + 0.5f);
            _previewTower.transform.localScale = new Vector3((range+0.5f)*2, 0.1f, (range+0.5f)*2);
        }
        
        public void ClearPreview() {
            if (_previewTower != null) {
                _previewTower.SetActive(false);
            }
        }
    }
}
