using UnityEngine;
using static TD.Tools.ScEnums;

namespace TD.Tower {
    public interface IPlaceableTower {
        public GameObject Prefab { get; set; }
        public string Name { get; set; }
        public TowerType Type { get; set; }
        public string Description { get; set; }
        public Sprite Icon { get; set; }
        public int BaseCost { get; set; }
        
        public int Range { get; set; }
    }
}