using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct SpawnSystem : ISystem
{
    uint updateCounter;
    bool hasSpawned;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // This call makes the system not update unless at least one entity in the world exists that has the Spawner component.
        state.RequireForUpdate<SpawnerEntity>();
        hasSpawned = false;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (!hasSpawned)
        {
            hasSpawned = true;
            var entityLookup = SystemAPI.GetSingletonEntity<SpawnerEntity>(); 
            var buffer = SystemAPI.GetBuffer<EntityBufferLookup>(entityLookup);
            var random = Random.CreateFromIndex(updateCounter++);

            for (int i=0; i < 100000; i++)
            {
                Entity entity = state.EntityManager.Instantiate(buffer[random.NextInt(buffer.Length)].Entity);
                var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                float2 position = (random.NextFloat2() - new float2(0.5f, 0.5f)) * 1000;
                transform.ValueRW.Position = new float3(position.x, 0, position.y);
                transform.ValueRW = transform.ValueRO.RotateY(random.NextFloat() * 360f);
            }
        }
    }
}

