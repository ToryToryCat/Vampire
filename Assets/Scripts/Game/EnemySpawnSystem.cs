using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemySpawnSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        float dt = Time.DeltaTime;

        Entities.ForEach((ref EnemySpawnConfig config, in LocalTransform transform) =>
        {
            config.Timer += dt;
            if (config.Timer >= config.Interval)
            {
                config.Timer = 0f;
                Entity enemy = ecb.Instantiate(config.Prefab);

                float3 offset = new float3(
                    UnityEngine.Random.Range(-config.Radius, config.Radius),
                    UnityEngine.Random.Range(-config.Radius, config.Radius),
                    0f);

                ecb.SetComponent(enemy, LocalTransform.FromPosition(transform.Position + offset));
            }
        }).Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}

