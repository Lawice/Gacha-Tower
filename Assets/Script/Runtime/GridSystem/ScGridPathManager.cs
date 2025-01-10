using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

namespace TD.Runtime.GridSystem {
    public class ScGridPathManager :MonoBehaviour {
        public static ScGridPathManager Instance;
        ScGridManager _gridManager => ScGridManager.Instance;

        public ScGridPath StartPath;
        public ScGridPath EndPath;
        
        public List<ScGridTile> Path = new();
        
        int _pathIndex = 0;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        private void Start() {
            Path = FindPath();
        }

        private List<ScGridTile> FindPath() {
            ScGridTile startTile = null;
            for (int x = 0; x < _gridManager.Grid.GetLength(0); x++) {
                for (int y = 0; y < _gridManager.Grid.GetLength(1); y++) {
                    ScGridPath pathComponent = _gridManager.Grid[x, y].GetComponent<ScGridPath>();
                    if (pathComponent == null || !pathComponent.IsStart) continue;
                    StartPath = pathComponent;
                    startTile = _gridManager.Grid[x, y];
                    break;
                }
            }

            if (startTile == null) {
                return null;
            }
            
            List<ScGridTile> path = new ();
            HashSet<ScGridTile> visited = new ();
            FindPathRecursive(startTile, path, visited);

            return path;
        }

        private bool FindPathRecursive(ScGridTile currentTile, List<ScGridTile> path, HashSet<ScGridTile> visited) {
            path.Add(currentTile);
            visited.Add(currentTile);

            ScGridPath currentPath = currentTile.GetComponent<ScGridPath>();
            currentPath.PathNumber = _pathIndex++;
            if (currentPath != null && currentPath.IsEnd) {
                EndPath = currentPath;
                return true;
            }
            
            Vector2Int[] directions = {
                new (0, 1), 
                new (0, -1), 
                new (-1, 0), 
                new (1, 0)   
            };

            foreach (Vector2Int dir in directions) {
                int newX = currentTile.TilePosition.x + dir.x;
                int newY = currentTile.TilePosition.y + dir.y;

                if (_gridManager.OutOfBounds(newX, newY) || visited.Contains(_gridManager.Grid[newX, newY])) {
                    continue;
                }
                ScGridTile neighbor = _gridManager.Grid[newX, newY];
                ScGridPath neighborPath = neighbor.GetComponent<ScGridPath>();
                if (neighborPath == null) {
                    continue;
                }
                if (FindPathRecursive(neighbor, path, visited)) {
                    return true;
                }
            }
            
            path.Remove(currentTile);
            return false;
        }
        
    }

}
