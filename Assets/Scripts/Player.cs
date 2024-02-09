using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CubePrefab;

    [SerializeField]
    private Animator m_AnimatedModel;

    [SerializeField]
    private GameObject m_CubesParent;

    [SerializeField]
    private List<Transform> m_ListOfCubes = new List<Transform>();

    public void RemoveCubeWithIndex(int cubeIndex)
    {
        Assert.IsTrue(cubeIndex < 0, "invalid index");
        Assert.IsTrue(cubeIndex > m_ListOfCubes.Count, "invalid index");

        GameObject.Destroy(m_ListOfCubes[cubeIndex]);
        m_ListOfCubes.RemoveAt(cubeIndex);     
    }

    private void RecalculateOffsets()
    {
        
    }


    private void Update()
    {
        
    }
}
