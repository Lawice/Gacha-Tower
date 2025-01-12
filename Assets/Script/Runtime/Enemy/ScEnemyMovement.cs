using System;
using System.Collections;
using System.Collections.Generic;
using TD.Runtime.GridSystem;
using UnityEngine;
using UnityEngine.AI;

namespace TD.Runtime.Enemy {
    [RequireComponent(typeof(NavMeshAgent), typeof(ScEnemy))]
    public class ScEnemyMovement : MonoBehaviour {
        NavMeshAgent _agent;
        private ScEnemy _enemy;
        
        List<Vector3> _waypoints = new ();
        
        public float TotalDistance; 
        Vector3 _previousPosition;

        
        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _enemy = GetComponent<ScEnemy>();
            _previousPosition = transform.position;
            StartCoroutine(LateStart());
        }

        IEnumerator LateStart() {
            
            yield return null;
            _agent.speed = _enemy.Enemy.Speed;
            foreach (ScGridTile tilePath in ScGridPathManager.Instance.Path) {
                _waypoints.Add(tilePath.transform.position);
            }
            
            StartCoroutine(FollowPath());
        }

        IEnumerator FollowPath() {
            if (_waypoints == null || _waypoints.Count == 0)
                yield break;

            foreach (Vector3 waypoint in _waypoints) {
                while (!_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance) {
                    yield return null;
                }
                _agent.SetDestination(waypoint);
            }
        }
        
        private void Update() {
            TotalDistance += Vector3.Distance(transform.position, _previousPosition);
            _previousPosition = transform.position;
        }
    }
}