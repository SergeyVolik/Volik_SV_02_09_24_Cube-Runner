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
    private Transform m_CubesParent;

    [SerializeField]
    private List<Transform> m_ListOfCubes = new List<Transform>();

    const float CUBE_HEIGHT = 1f;

    private CubeCharacterController m_CharController;

    private void Awake()
    {
        m_CharController = GetComponent<CubeCharacterController>();
    }

    public void RemoveCubeWithIndex(int cubeIndex)
    {
        Assert.IsTrue(cubeIndex < 0, "invalid index");
        Assert.IsTrue(cubeIndex > m_ListOfCubes.Count, "invalid index");

        GameObject.Destroy(m_ListOfCubes[cubeIndex]);
        m_ListOfCubes.RemoveAt(cubeIndex);     
    }

    private void AddCube()
    {
        float cubeYOffset = m_ListOfCubes.Count * CUBE_HEIGHT;
        var spawnPos = new Vector3(0, cubeYOffset, 0);
        var cubeInstance = GameObject.Instantiate(m_CubePrefab, spawnPos, Quaternion.identity, m_CubesParent);

        m_ListOfCubes.Add(cubeInstance.transform);
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        m_CharController.xInput = xInput;
    }
}
