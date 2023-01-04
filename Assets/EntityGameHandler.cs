using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

[GenerateAuthoringComponent]
public struct EntityGameHandler : IComponentData
{

    public Entity GameHandlerPrefab;

}
public struct GameInit : IComponentData
{

   

}

public partial class GameHandler: SystemBase
{
    private Entity player;
    public Entity GameHandlerEntity;
    public Entity playerEntity;
    protected override void OnCreate()
    {
        Enabled = false;
    }
    protected override void OnStartRunning()
    {
        EntityManager em = World.DefaultGameObjectInjectionWorld.EntityManager;
        playerEntity = em.Instantiate(GetSingleton<PlayerPrefabComponent>().PlayerPrefab);
        GameHandlerEntity = em.Instantiate(GetSingleton<EntityGameHandler>().GameHandlerPrefab);

      
        em.AddComponentData(GameHandlerEntity, new LevelComponent
        {
            wave = 1,
            enemies = 10,
        });

        em.AddComponentData(playerEntity, new PlayerComponent()
        {

        });
        em.AddComponentData(playerEntity, new ShootingComponent()
        {

        });
        em.SetComponentData(playerEntity, new Translation
        {
            Value = math.float3(0, 1, 0)
        });

        em.SetComponentData(playerEntity, new LocalToWorld
        {
            Value = math.float4x4(0, 0, 0, 0)
        });
    }

 
    protected override void OnUpdate()
    {
        

      
        Entities.WithoutBurst().ForEach(( ref GameInit init ) =>
        {

            // EntityManager.SetComponentData(World.GetExistingSystem<MoveSystem>().playerEntity,  new Translation 
            // {
            //     Value = math.float3(0, 1, 0)
            // });
            //    EntityManager.RemoveComponent<GameInit>(World.GetExistingSystem<MoveSystem>().GameHandlerEntity);
  
        

        }).Run();


        if (HasComponent<isDeadTag>(World.GetExistingSystem<GameHandler>().playerEntity))
        {

            Enabled = false;

        }
    }
}
