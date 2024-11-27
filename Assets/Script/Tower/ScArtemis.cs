using TD.Tools;
using UnityEngine;

namespace TD.Tower {
    public class ScArtemis: MonoBehaviour, ITower {
        [field:SerializeField]public Vector2Int Position { get; set; }
        [field: SerializeField] public int Range { get; set; } = 40;
        [field: SerializeField] public int Cooldown { get; set; } = 2;
        [field: SerializeField] public int Level { get; set; } = 1;
        [field:SerializeField]public ScEnums.Rarity Rarity { get; set; }
        [field:SerializeField]public SoTower Tower { get; set; }
        public GameObject projectilePrefab;
        
        public void Attack() {
            
        }

        public void Evolve() {
            
        }

        public void Upgrade() {
            
        }
    }
}