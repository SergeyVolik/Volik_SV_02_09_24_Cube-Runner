using CubeRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeHealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthCube m_CubePrefab;

    [SerializeField]
    private List<HealthCube> m_ListOfCubes = new List<HealthCube>();

    public IEnumerable<HealthCube> GetHealthCubes() => m_ListOfCubes;

    [SerializeField]
    private Transform m_CubesParent;

    const float CUBE_HEIGHT = 1f;

    public event Action onDeath = delegate { };
    public event Action onCubeAdded = delegate { };
    public event Action onCubeRemoved = delegate { };

    public Vector3 GetNextTopPosition() => GetTopCube().transform.position + new Vector3(0, CUBE_HEIGHT, 0);
    public float GetHeight() => m_ListOfCubes.Count * CUBE_HEIGHT;
    private HealthCube GetTopCube() => m_ListOfCubes[m_ListOfCubes.Count - 1];
    public bool IsTopCube(HealthCube healthCube)
    {
        float yPos = healthCube.transform.position.y;

        foreach (var item in GetHealthCubes())
        {
            var cubeYPos = item.transform.position.y;

            if (yPos < cubeYPos)
            {
                return false;
            }
        }

        return true;
    }

    public void RemoveCube(HealthCube cube)
    {
        if (cube.transform.parent == null)
            return;

        DelayExecutor.Execute(1f, () =>
        {
            cube.gameObject.SetActive(false);
        });

        cube.transform.parent = null;
        m_ListOfCubes.Remove(cube);
        onCubeRemoved.Invoke();
    }

    public void AddCube()
    {
        var cubeHolderOffset = m_CubesParent.transform.localPosition;

        var cubesHeight = m_ListOfCubes.Count * CUBE_HEIGHT;

        float cubeYOffset = cubesHeight - cubeHolderOffset.y;
        var spawnPos = m_CubesParent.position + new Vector3(0, cubeYOffset, 0);

        var cubeInstance = GameObjectPool.GetPoolObject(m_CubePrefab);
        cubeInstance.gameObject.SetActive(true);
        cubeInstance.transform.SetParent(m_CubesParent);
        cubeInstance.transform.position = spawnPos;
        m_ListOfCubes.Add(cubeInstance);

        m_ListOfCubes.OrderBy((e) => e.transform.position.x);

        onCubeAdded.Invoke();
    }

    public void ExecuteDeath()
    {
        onDeath.Invoke();
    }
}
