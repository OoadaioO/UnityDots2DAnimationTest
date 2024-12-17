using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[RequireMatchingQueriesForUpdate]
partial struct SpawnerSystem : ISystem
{


	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{

		RefRW<Spawner> spawner = SystemAPI.GetSingletonRW<Spawner>();

		if (spawner.ValueRO.spawnCount >= spawner.ValueRO.spawnCountMax)
		{
			return;
		}

		float3 lowerLeft = new float3(-1,-3,0) ;

		int columnCount = spawner.ValueRO.columnCount;
		for (int i = spawner.ValueRO.spawnCount; i < spawner.ValueRO.spawnCountMax; i++)
		{
			spawner.ValueRW.spawnCount++;
			Entity entity = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
			RefRW<LocalTransform> localTransform = SystemAPI.GetComponentRW<LocalTransform>(entity);

			int row = i / columnCount;
			int column = i % columnCount;

			localTransform.ValueRW.Position = new float3(row*0.1f, column*0.1f, 0) + lowerLeft;

		}
	}


}
