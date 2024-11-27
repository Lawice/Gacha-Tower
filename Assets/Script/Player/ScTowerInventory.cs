using UnityEngine;
using AYellowpaper.SerializedCollections;
using static TD.Tools.ScEnums;
using TD.Tower;

namespace TD.Player {
    public class ScTowerInventory : MonoBehaviour {
        public SerializedDictionary<TowerItem, int> Towers = new();
    }

    [System.Serializable]
    public struct TowerItem {
        public SoTower Tower;
        public Rarity Rarity;
        public int Level;
    }
}