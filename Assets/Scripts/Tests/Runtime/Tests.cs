using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator Test_Spawn_Player_Trigger_Collectable_Cube_Then_Assert_CubsNumber()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator Spawn_Player_Then_Spawn_Location_With_Obstacle_Assert_Death()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator Spawn_Player_Then_Spawn_Location_Then_Assert()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        // 2
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        //game = gameGameObject.GetComponent<Game>();
        // 3
        //GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // 4
        //float initialYPos = asteroid.transform.position.y;
        // 5
        yield return new WaitForSeconds(0.1f);
        // 6
        //Assert.Less(asteroid.transform.position.y, initialYPos);
        // 7
        //Object.Destroy(game.gameObject);
    }
}
