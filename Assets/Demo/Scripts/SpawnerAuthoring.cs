using Unity.Entities;
using UnityEngine;

   public class SpawnerAuthoring : MonoBehaviour {
        public int spawnCountMax;
        public int columnCount;
        public GameObject prefabGameObject;
        public class Baker : Baker<SpawnerAuthoring> {
            public override void Bake(SpawnerAuthoring authoring) {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Spawner {
                    prefab = GetEntity(authoring.prefabGameObject, TransformUsageFlags.Dynamic),
                    spawnCountMax = authoring.spawnCountMax,
                    columnCount = authoring.columnCount
                });
            }
        }
    }
    public struct Spawner : IComponentData {
        public Entity prefab;
        public int spawnCountMax;
        public int spawnCount;
        public int columnCount;
    }
