using UnityEngine;
using static TD.Runtime.Tools.ScEnums;

namespace TD.Runtime.Tower {
    public interface ITower {
        public Vector2Int Position { get; set; }
        
        public int Range { get; set; }
        public int Cooldown{get; set;}
        
        public int Level { get; set; }

        public int MaxLevel { get; set; }
        
        public Rarity Rarity { get; set; }
        public SoTower Tower { get; set; }
        
        public void Attack();
        public void Upgrade(int levelAmount = 1);

        public void InitTower(Vector2Int position, Rarity rarity, int level, int range) {
            Position = position;
            Rarity = rarity;
            Level = level;
            Range = range;
        }
    }
}