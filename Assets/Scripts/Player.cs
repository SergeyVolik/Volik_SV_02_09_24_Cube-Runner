using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace CubeRunner
{
    [RequireComponent(typeof(CubeCharacterController))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private HealthCube m_CubePrefab;

        [SerializeField]
        private Animator m_AnimatedModel;

        [SerializeField]
        private ParticleSystem m_ParticleSystem;

        [SerializeField]
        private Transform m_CubesParent;

        [SerializeField]
        private List<HealthCube> m_ListOfCubes = new List<HealthCube>();

        public IEnumerable<HealthCube> GetHealthCubes() => m_ListOfCubes;

        const float CUBE_HEIGHT = 1f;

        private CubeCharacterController m_CharController;
        private Vector3 m_StartModelPosition;

        private void Awake()
        {
            m_CharController = GetComponent<CubeCharacterController>();
            m_StartModelPosition = m_AnimatedModel.transform.localPosition;
        }

        public void RemoveCube(HealthCube cube)
        {
            if (cube.transform.parent == null)
                return;

            GameObject.Destroy(cube, 1f);
            cube.transform.parent = null;
            m_ListOfCubes.Remove(cube);
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

            m_ListOfCubes.Add(cubeInstance);
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
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<LocationObstacle>(out var obs))
            {
                obs.Visit(this);
            }

            if (other.TryGetComponent<CollectableObject>(out var collObj))
            {
                collObj.Visit(this);
            }
        }
    }
}