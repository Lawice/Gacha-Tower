using System;
using TD.Runtime.Tower;
using UnityEngine;

namespace Script.Runtime.Enemy {
    public class ScEnemy : MonoBehaviour {
        [SerializeField] private int _moneyReward;

        private void OnDestroy() {
            ScTowerManager.Instance.AddMoney(_moneyReward);
        }
    }
}