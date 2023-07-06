using Unity.Collections;
using Unity.Entities;
using UnityEngine;

// An authoring component is just a normal MonoBehavior that has a Baker<T> class.
public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject[] Prefabs;

    // In baking, this Baker will run once for every SpawnerAuthoring instance in a subscene.
    // (Note that nesting an authoring component's Baker class inside the authoring MonoBehaviour class
    // is simply an optional matter of style.)
    class Baker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            
            var prefabsMap = new NativeHashMap<int, Entity>(128, Allocator.Persistent);
            for (int i = 0; i < authoring.Prefabs.Length; i++)
            {
                prefabsMap.Add(i, GetEntity(authoring.Prefabs[i], TransformUsageFlags.Dynamic));
            }

            var buffer = AddBuffer<EntityBufferLookup>(entity);
            for (int i = 0; i < authoring.Prefabs.Length; i++)
            {
                var prefab = GetEntity(authoring.Prefabs[i], TransformUsageFlags.Dynamic);
                buffer.Add(new EntityBufferLookup() { Entity = prefab });
            }

            AddComponent(entity, new SpawnerEntity
            {
            });
        }


    }
}

struct SpawnerEntity : IComponentData
{
}
public struct EntityBufferLookup : IBufferElementData
{
    public Entity Entity;
}