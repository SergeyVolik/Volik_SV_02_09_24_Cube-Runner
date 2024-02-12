using CubeRunner;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests
{
    private const string ObstancePath = "WallObstacle/ModularWall";
    private const string ObstacleBlockPath = "WallObstacle/WallBlock";
    private const string PlayerPath = "Player";
    private const string CollectablePath = "CollectableCube";
    private const string TrackGroundPath = "TrackGround";

    Camera camera;

    [SetUp]
    public void Setup()
    {
        GameObject cameraObj = new GameObject("Camera");

        camera = cameraObj.AddComponent<Camera>();
        camera.transform.position = new Vector3(10, 10, -10);
    }

    [UnityTest]
    public IEnumerator Test_Spawn_Player_Trigger_Collectable_Cube_Then_Assert_NumberOfCubes()
    {
        yield return new WaitForSeconds(1f);
        //setup
        GameObject playerPrefab =
         Resources.Load<GameObject>(PlayerPath);

        GameObject collectableCubePrefab =
            Resources.Load<GameObject>(CollectablePath);

        var playerInstance = GameObject.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        camera.transform.LookAt(playerInstance.transform.position);

        var collectableInstance = GameObject.Instantiate(collectableCubePrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //test
        var healthBar = playerInstance.GetComponent<CubeHealthBar>();
        var initNumberOfCubes = healthBar.NumberOfCubes;
        var targetResult = initNumberOfCubes + 1;
        yield return null;

        Assert.IsTrue(healthBar.NumberOfCubes == targetResult, "Target health assert failed");

        yield return new WaitForSeconds(1f);

        //cleanup
        GameObject.Destroy(playerInstance);
        GameObject.Destroy(collectableInstance);
    }

    [UnityTest]
    public IEnumerator Test_Spawn_Player_Then_Spawn_Obst_Then_Assert_Death()
    {
        yield return SpawnPlayerAndObstacleTest(Vector3.zero);
    }

    [UnityTest]
    public IEnumerator Test_Spawn_Player_Then_Spawn_Obst_UnderPlayer_Then_Assert_Death()
    {
        yield return SpawnPlayerAndObstacleTest(new Vector3(0, 2f, 0));
    }

    IEnumerator SpawnPlayerAndObstacleTest(Vector3 obstaclePos)
    {
        //setup
        GameObject obstaclePrefab =
          Resources.Load<GameObject>(ObstancePath);

        var obstacleInstance = GameObject.Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);
        camera.transform.LookAt(obstacleInstance.transform.position);

        yield return new WaitForSeconds(1f);

        //spawn player
        GameObject playerPrefab = Resources.Load<GameObject>(PlayerPath);
        var playerInstance = GameObject.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
       
        //test
        yield return null;

        var character = playerInstance.GetComponent<CubeCharacter>();
        Assert.IsTrue(character.IsDead == true, "asset death failed");

        yield return new WaitForSeconds(1f);

        //cleanup
        GameObject.Destroy(playerInstance);
        GameObject.Destroy(obstacleInstance);
    }


    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(camera.gameObject);
    }
}
