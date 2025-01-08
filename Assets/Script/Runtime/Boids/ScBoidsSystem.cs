using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD.RuntimeBoids {
    public class ScBoidsSystem : MonoBehaviour {
        public Transform BoidPrefab;
        public Transform Attractor;
        [SerializeField] private int _boidsCount;

        Transform AttractionPoint;

        [SerializeField] BoidSettings _settings;

        Boid[] _boids;

        BoidRegion _regions = new();

        private void Start() {
            _boids = new Boid[_boidsCount];
            _regions.Add(Vector3Int.zero, new());

            for (int u = 0; u < _boidsCount; u++) {
                Transform boid = Instantiate(BoidPrefab, transform);
                foreach (var child in boid.GetComponentsInChildren<MeshRenderer>()) {
                    child.material.color = new Color(Random.value, Random.value, Random.value);
                }
                _boids[u] = new Boid { Transform = boid, Velocity = Random.onUnitSphere };
                _regions[Vector3Int.zero].Add(_boids[u]);
            }
        }

        private void Update() {
            ComputeNextVelocities();
            ApplyNextVelocities();
        }

        private void ComputeNextVelocities() {
            _settings.Attractor = Attractor ? Attractor.position : Vector3.zero;
            
            Parallel.For(0, _boids.Length, u => {
                _boids[u].ComputeNextVelocity(_settings, _regions);
            });
        }

        private void ApplyNextVelocities() {
            for (int u = 0; u < _boids.Length; u++) {
                _boids[u].ApplyNextVelocity(_settings, _regions);
            }
        }

        struct Boid : IEquatable<Boid> {
            public Transform Transform;
            public Vector3 Position;
            public Vector3 Velocity;
            public Vector3 NextVelocity;

            Vector3Int _region;

            public void ComputeNextVelocity(BoidSettings settings, BoidRegion regions) {
                Vector3 alignment = Vector3.zero;
                Vector3 avoidance = Vector3.zero;
                Vector3 cohesion = Vector3.zero;
                Vector3 attraction = Vector3.zero;

                int count = 0;
                
                foreach (Boid boid in regions.GetBoidNearTo(_region)) {
                    if (Equals(boid, this)) {
                        continue;
                    }

                    //alignment
                    Vector3 direction = boid.Velocity;
                    float distance = Vector3.Distance(Position, boid.Position);
                    alignment += Vector3.ClampMagnitude(direction / Mathf.Max(distance, 0.01f), 1);

                    //avoidance
                    direction = Position - boid.Position;
                    distance = direction.magnitude / settings.FarThreshold;
                    avoidance += direction.normalized * (1 - distance);

                    //cohesion
                    direction *= -1;
                    if (distance > settings.FarThreshold) {
                        cohesion += Vector3.ClampMagnitude(direction.normalized * (distance - 1), 1);
                    }
                    
                    //attraction
                    attraction = settings.Attractor - Position;

                    if (count++ > settings.MaxIteration) {
                        break;
                    }
                }

                NextVelocity = alignment * settings.Alignment + avoidance * settings.Avoidance +
                               cohesion * settings.Cohesion + attraction.normalized * settings.Attraction;
                NextVelocity.Normalize();
            }

            public void ApplyNextVelocity(BoidSettings settings, BoidRegion regions) {
                Velocity = Vector3.Slerp(Velocity, NextVelocity, settings.TurnRate);
                Position =Transform.position += Velocity * (settings.Speed * Time.deltaTime);
                Transform.forward = Velocity;

                Vector3Int newRegion = new(
                    Mathf.FloorToInt(Position.x / settings.FarThreshold),
                    Mathf.FloorToInt(Position.y / settings.FarThreshold),
                    Mathf.FloorToInt(Position.z / settings.FarThreshold)
                );

                if (newRegion == _region) return;
                
                regions[_region].Remove(this);
                
                if (!regions.ContainsKey(newRegion)) {
                    regions[newRegion] = new();
                }

                regions[newRegion].Add(this);
                _region = newRegion;
            }

            public bool Equals(Boid other) {
                return Equals(Transform, other.Transform) && Velocity.Equals(other.Velocity) && NextVelocity.Equals(other.NextVelocity) && _region.Equals(other._region);
            }

            public override bool Equals(object obj) {
                return obj is Boid other && Equals(other);
            }

            public override int GetHashCode() {
                return HashCode.Combine(Transform, Velocity, NextVelocity, _region);
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
            public int MaxIteration;

            public Vector3 Attractor;
        }

        class BoidRegion : Dictionary<Vector3Int, List<Boid>> {
            public IEnumerable<Boid> GetBoidNearTo(Vector3Int region) {
                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        for (int z = -1; z <= 1; z++) {
                            Vector3Int testedRegion = region + new Vector3Int(x, y, z);
                            if (!ContainsKey(testedRegion)) {
                                continue;
                            }

                            foreach (Boid boid in this[testedRegion]) {
                                yield return boid;
                            }
                        }
                    }
                }
            }
        }
    }
}


