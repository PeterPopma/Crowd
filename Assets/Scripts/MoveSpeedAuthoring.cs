using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

// An authoring component is just a normal MonoBehavior.
public class MoveSpeedAuthoring : MonoBehaviour
{
    public float Speed = 0.0f;

    // In baking, this Baker will run once for every MoveSpeedAuthoring instance in an entity subscene.
    // (Nesting an authoring component's Baker class is simply an optional matter of style.)
    class Baker : Baker<MoveSpeedAuthoring>
    {
        public override void Bake(MoveSpeedAuthoring authoring)
        {
            // The entity will be moved
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MoveSpeed
            {
                Speed = authoring.Speed
            });
        }
    }
}

public struct MoveSpeed : IComponentData
{
    public float Speed;
}
