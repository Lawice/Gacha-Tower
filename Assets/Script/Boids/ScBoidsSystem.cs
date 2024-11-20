using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD.Boids {
    public class ScBoidsSystem : MonoBehaviour {
        public Transform BoidPrefab;
        public int BoidsCount;
    
        Transform AttractionPoint;
        
        Boid[] _boids;
        
        [SerializeField] BoidSettings _settings;

        private void Start() {
            _boids = new Boid[BoidsCount];
        
            for (int u = 0; u < BoidsCount; u++) {
                _boids[u] = new Boid{Transform = Instantiate(BoidPrefab, transform), Velocity = Random.onUnitSphere};
            }
        }

        private void Update() {
            ComputeNextVelocities();
            ApplyNextVelocities();
        }

        private void ComputeNextVelocities() {
            for(int u = 0; u < _boids.Length; u++) {
                _boids[u].ComputeNextVelocity(_boids, _settings);
            }
        }
        private void ApplyNextVelocities() {
            for(int u = 0; u < _boids.Length; u++) {
                _boids[u].ApplyNextVelocity(_boids, _settings);
            }
        }

        struct Boid {
            public Transform Transform;
            public Vector3 Velocity;
            public Vector3 NextVelocity;

            public void ComputeNextVelocity(Boid[] boids, BoidSettings settings) {
                Vector3 alignment = Vector3.zero;
                Vector3 avoidance = Vector3.zero;
                Vector3 cohesion = Vector3.zero;
                
                for(int u = 0; u < boids.Length; u++) {
                    if (Equals(boids[u], this)) {
                        continue;
                    }
                    
                    //alignment
                    alignment += boids[u].Velocity;
                    
                    //avoidance
                    Vector3 direction = Transform.position - boids[u].Transform.position;
                    float distance = direction.magnitude / settings.FarThreshold;
                    avoidance += direction.normalized * (1 - distance);
                    
                    //cohesion
                    direction *= -1;
                    if (distance > settings.FarThreshold) {
                        cohesion += Vector3.ClampMagnitude(direction.normalized * (distance - 1),1);
                    }

                }
                
                NextVelocity = alignment*settings.Alignment + avoidance*settings.Avoidance + cohesion*settings.Cohesion;
                NextVelocity.Normalize();
            }

            public void ApplyNextVelocity(Boid[] boids, BoidSettings settings) {
                Velocity = Vector3.Slerp(Velocity, NextVelocity, settings.TurnRate);
                Transform.position += Velocity * (settings.Speed * Time.deltaTime);
            }
        }

        [Serializable]
        class BoidSettings {
            public float Alignment;
            public float Avoidance;
            public float Attraction;
            public float Cohesion;
            public float FarThreshold;
            public float Speed;
            public float TurnRate;
        }
    }
}


