using AYellowpaper.SerializedCollections;
using UnityEngine;
using static TD.Runtime.Tools.ScEnums;

namespace TD.Runtime.Enemy {
    public class ScEnemyPoolManager : MonoBehaviour {
        public SerializedDictionary<SoEnemy, ScEnemyPool> EnemyPools;

        public ScEnemyPool GetPool(SoEnemy enemy) {
            return EnemyPools[enemy];
        }
        
        
    }
}