using System;
using System.Collections;
using System.Collections.Generic;
using TD.Runtime.GridSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Runtime.Enemy {
    [RequireComponent(typeof(NavMeshAgent))]
    public class ScEnemyMovement : MonoBehaviour {
        NavMeshAgent _agent;
        
        List<Vector3> _waypoints = new ();
        

        
        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            StartCoroutine(LateStart());
        }

        IEnumerator LateStart() {

            yield return null;

            foreach (ScGridTile tilePath in ScGridPathManager.Instance.Path) {
                _waypoints.Add(tilePath.transform.position);
            }
            
            StartCoroutine(FollowPath());
        }

        IEnumerator FollowPath() {
            if (_waypoints == null || _waypoints.Count == 0)
                yield break;

            for (int index = 0; index < _waypoints.Count; index++) {
                Debug.Log(index);
                Vector3 waypoint = _waypoints[index];
                

                while (!_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance) {
                    yield return null;
                }
                _agent.SetDestination(waypoint);
            }
        }
    }
}