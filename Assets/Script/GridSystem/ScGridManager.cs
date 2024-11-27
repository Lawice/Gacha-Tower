using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TD.GridSystem {
    public class ScGridManager : MonoBehaviour {
        public static ScGridManager Instance { get; private set; }
        
        [SerializeField] Transform _ground;
        public int TileSize = 1;
        ScGridTile[,] _grid;

        public Vector2Int GridSize;
        private ScGridCursor _cursor;
        
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
            if (x < 0 || y < 0 ||x >=GridSize.x || y >= GridSize.y) return null;
            return _grid[x, y];
        }

    }
}
