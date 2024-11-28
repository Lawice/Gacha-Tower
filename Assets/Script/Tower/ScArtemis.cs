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
        public int MaxLevel { get; set; } = 10;
        [field:SerializeField] public GameObject projectilePrefab;

        
        public void Attack() {
            
        }


        public void Evolve(int amount = 1) {
            if (Rarity + amount > ScEnums.Rarity.Heroic) {
                Rarity = ScEnums.Rarity.Heroic;
            }
            Rarity += amount;
        }

        public void Upgrade(int levelAmount = 1) {
            if(Level + levelAmount > MaxLevel) Level = MaxLevel;
            Level += levelAmount;
        }
 c7b8b10 (Artemis V1)
        public void Evolve() {
            
        }

        public void Upgrade() {
            
        }
    }
}