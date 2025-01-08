using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Runtime.Tower.View {
    public class ScFieldOfView : MonoBehaviour    {
        public float ViewRadius;
        [Range(0, 360)]
        public float ViewAngle;
        
        [SerializeField] LayerMask _targetMask;
        [SerializeField] LayerMask _obstacleMask;

        public List<Transform> VisibleTargets = new ();

        public float MeshResolution;
        
        [SerializeField] MeshFilter _viewMeshFilter;
        Mesh _viewMesh;
        
        private void Start() {
            _viewMesh = new Mesh {
                name = "View Mesh"
            };
            _viewMeshFilter.mesh = _viewMesh;
            StartCoroutine(nameof(FindTargetsWithDelay), 0.2f);
        }

        private void LateUpdate() {
            DrawFieldOfView();
        }

        IEnumerator FindTargetsWithDelay(float delay) {
            while (true) {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }
        
        
        void FindVisibleTargets() {
            HashSet<Transform> newVisibleTargets = new();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, _targetMask);

            foreach (Collider collider in targetsInViewRadius) {
                Transform target = collider.transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if (Vector3.Dot(transform.forward, dirToTarget) <= Mathf.Cos(ViewAngle * 0.5f * Mathf.Deg2Rad))
                    continue;

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, _obstacleMask)) {
                    newVisibleTargets.Add(target);
                }
            }


            VisibleTargets = new List<Transform>(newVisibleTargets);
        }
        
        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal) {
            if (!angleIsGlobal) {
                angleInDegrees += transform.eulerAngles.y;
            }
            float angleInRads = angleInDegrees * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(angleInRads), 0, Mathf.Cos(angleInRads));
        }
        
        void DrawFieldOfView() {
            int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);
            float stepAngleSize = ViewAngle / stepCount;
            List<Vector3> viewPoints = new();
            
            for (int i = 0; i <= stepCount; i++) {
                float angle = transform.eulerAngles.y - ViewAngle / 2 + stepAngleSize * i;
                StViewCastInfo viewCast = SetViewCast(angle);
                viewPoints.Add(viewCast.Point);
            }
            
            int vertexCount = viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (int j = 0; j < vertexCount - 1; j++) {
                vertices[j + 1] =  transform.InverseTransformPoint(viewPoints[j]);

                if (j >= vertexCount - 2) continue;
                
                triangles[j*3] = 0;
                triangles[j*3 + 1] = j + 1;
                triangles[j*3 + 2] = j + 2;
            }
            
            
            
            _viewMesh.Clear();
            _viewMesh.vertices = vertices;
            _viewMesh.triangles = triangles;
            
            Debug.Log(_viewMesh.vertices.Length + " " + _viewMesh.triangles.Length);
            _viewMesh.RecalculateNormals();
        }
        
        StViewCastInfo SetViewCast(float globalAngle) {
            Vector3 direction = DirectionFromAngle(globalAngle, true);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, ViewRadius, _obstacleMask)) {
                return new StViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            return new StViewCastInfo(false, transform.position + direction * ViewRadius, ViewRadius, globalAngle);
        }
    }
}