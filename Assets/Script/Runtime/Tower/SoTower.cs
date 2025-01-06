using UnityEngine;
using static TD.Runtime.Tools.ScEnums;

namespace TD.Runtime.Tower {
    [CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Tower", order = 0)]
    public class SoTower : ScriptableObject, IPlaceableTower {
        [field:SerializeField]public string Name { get; set; }
        [field:SerializeField]public TowerType Type { get; set; }
        [field:SerializeField]public string Description { get; set; }
        [field:SerializeField]public Sprite Icon { get; set; }
        [field:SerializeField]public GameObject Prefab { get; set; }
        [field:SerializeField]public int BaseCost { get; set; }
        [field:SerializeField]public int Range { get; set; }
        
    }
}