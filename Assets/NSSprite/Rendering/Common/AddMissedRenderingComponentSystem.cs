using Unity.Burst;
using Unity.Entities;

namespace NSprites
{
#if UNITY_EDITOR
	[UpdateInGroup(typeof(PresentationSystemGroup))]
	[UpdateBefore(typeof(SpriteRenderingSystem))]
	[WorldSystemFilter(WorldSystemFilterFlags.Default | WorldSystemFilterFlags.Editor | WorldSystemFilterFlags.EntitySceneOptimizations)]
	public partial class AddMissedRenderingComponentSystem : SystemBase  // 改为继承 SystemBase
	{
		private EntityQuery _query;

		protected override void OnCreate()  // 修改方法签名
		{
			_query = SystemAPI.QueryBuilder()
				.WithAll<PropertyPointer>()
				.WithNoneChunkComponent<PropertyPointerChunk>()
				.WithOptions(EntityQueryOptions.IncludePrefab | EntityQueryOptions.IncludeDisabledEntities | EntityQueryOptions.Default | EntityQueryOptions.IgnoreComponentEnabledState)
				.Build();

			RequireForUpdate(_query);   // 移除 state 参数
		}

		protected override void OnUpdate()  // 修改方法签名
		{
			EntityManager.AddChunkComponentData(_query, new PropertyPointerChunk());
		}
	}
#else
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateBefore(typeof(SpriteRenderingSystem))]
    [WorldSystemFilter(WorldSystemFilterFlags.Default | WorldSystemFilterFlags.Editor | WorldSystemFilterFlags.EntitySceneOptimizations)]
    public partial struct AddMissedRenderingComponentSystem : ISystem
    {
        private EntityQuery _query;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state) 
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<PropertyPointer>()
                .WithNoneChunkComponent<PropertyPointerChunk>()
                .WithOptions(EntityQueryOptions.IncludePrefab | EntityQueryOptions.IncludeDisabledEntities | EntityQueryOptions.Default | EntityQueryOptions.IgnoreComponentEnabledState)
                .Build();
            
            state.RequireForUpdate(_query);   
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
            => state.EntityManager.AddChunkComponentData(_query, new PropertyPointerChunk());
    }
#endif
}