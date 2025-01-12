using System;
using System.Collections.Generic;
using TD.Runtime.Tools;
using TD.Runtime.Tower.View;
using UnityEngine;

namespace TD.Runtime.Tower {
    public abstract class ScTower :MonoBehaviour, ITower {
        [field:SerializeField] public Vector2Int Position { get; set; }
        [field:SerializeField] public float AttackCooldown { get; set; }
        [field:SerializeField] public int Level { get; set; }
        [field: SerializeField] public int MaxLevel { get; set; } = 10;
        [field:SerializeField] public ScEnums.Rarity Rarity { get; set; }
        [field:SerializeField] public SoTower Tower { get; set; }

        [SerializeField] protected ScFieldOfView _fov;
        
         protected List<Transform> _enemiesInRange = new();
         
         private void Start() {
             _fov.ViewRadius = Tower.Range;
             _fov.Showing = false;
         }
         
         protected void GetEnemiesInRange() {
             _enemiesInRange.Clear();
             _enemiesInRange.AddRange(_fov.VisibleTargets);
         }
        
        public virtual void Attack(Vector3 target) {
            
        }

        public virtual void Upgrade(int levelAmount = 1) {
            if (Level + levelAmount > MaxLevel) {
                Level = MaxLevel;
            }
            Level += levelAmount;
        }

        public virtual void ShowRange(bool show) {
            _fov.Showing = show;
        }
    }
}