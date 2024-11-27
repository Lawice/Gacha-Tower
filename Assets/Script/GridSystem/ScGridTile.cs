using UnityEngine;
using TD.Tower;

namespace TD.GridSystem {
    public class ScGridTile : MonoBehaviour {
        public Vector2Int TilePosition;
        public bool IsTowerPlaceable;
        public ITower TowerOnTile;
        
        private void Start() {
            TilePosition = new Vector2Int((int)(transform.localPosition.x), (int)(transform.localPosition.z));
        }

        public void OpenTowerSelection() {
            Debug.Log(TilePosition + "actual coord :"+ transform.localPosition);
            GetComponentInChildren<MeshRenderer>().material.color = Color.black;
        }
        
        
    }
}