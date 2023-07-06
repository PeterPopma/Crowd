using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MoveSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnerEntity>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, entity) in
                 SystemAPI.Query<RefRW<LocalTransform>>()
                     .WithAll<MoveSpeed>()
                     .WithEntityAccess())
        {
            transform.ValueRW.Position += transform.ValueRO.Forward() * SystemAPI.Time.DeltaTime * 180;
            if (transform.ValueRO.Position.x < -2500 || transform.ValueRO.Position.x > 2500 || transform.ValueRO.Position.z < -2500 || transform.ValueRO.Position.z > 2500)
            {
                transform.ValueRW = transform.ValueRO.RotateY(180);
            }
        }
    }
}