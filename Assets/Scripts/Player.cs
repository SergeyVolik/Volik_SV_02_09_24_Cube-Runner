using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(CubeCharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CubePrefab;

    [SerializeField]
    private Animator m_AnimatedModel;

    [SerializeField]
    private ParticleSystem m_ParticleSystem;

    [SerializeField]
    private Transform m_CubesParent;

    [SerializeField]
    private List<Transform> m_ListOfCubes = new List<Transform>();

    const float CUBE_HEIGHT = 1f;

    private CubeCharacterController m_CharController;
    private Vector3 m_StartModelPosition;

    private void Awake()
    {
        m_CharController = GetComponent<CubeCharacterController>();
        m_StartModelPosition = m_AnimatedModel.transform.localPosition;
    }

    public void RemoveCubeWithIndex(int cubeIndex)
    {
      
        Assert.IsTrue(cubeIndex < 0, "invalid index");
        Assert.IsTrue(cubeIndex > m_ListOfCubes.Count, "invalid index");

        GameObject.Destroy(m_ListOfCubes[cubeIndex]);
        m_ListOfCubes.RemoveAt(cubeIndex);     
    }

    public void AddCube()
    {
        var cubeHolderOffset = m_CubesParent.transform.localPosition;

        var cubesLen = m_ListOfCubes.Count * CUBE_HEIGHT;

        m_AnimatedModel.SetTrigger("Jump");
      
        var modelPos = m_StartModelPosition;
        modelPos.y += cubesLen;
        m_AnimatedModel.transform.localPosition = modelPos;

        m_ParticleSystem?.Play();

        float cubeYOffset = cubesLen - cubeHolderOffset.y;
        var spawnPos = m_CubesParent.position + new Vector3(0, cubeYOffset, 0);

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
