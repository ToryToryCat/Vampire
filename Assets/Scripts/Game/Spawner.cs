using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 0.5f;
    public float spawnRadius = 10f;

    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new EnemySpawnConfig
            {
                Prefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                Interval = authoring.spawnInterval,
                Radius = authoring.spawnRadius,
                Timer = 0f
            });
        }
    }
}

public struct EnemySpawnConfig : IComponentData
{
    public Entity Prefab;
    public float Interval;
    public float Radius;
    public float Timer;
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public int health;
    public float spawnTime;
    public float speed;
}

