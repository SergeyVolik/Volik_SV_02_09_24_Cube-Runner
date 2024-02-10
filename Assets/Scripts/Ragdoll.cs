using UnityEngine;

namespace CubeRunner
{
    public class Ragdoll : MonoBehaviour
    {
        private Rigidbody[] m_Bodies;
        private Collider[] m_Colliders;

        private void Awake()
        {
            m_Bodies = GetComponentsInChildren<Rigidbody>();
            m_Colliders = GetComponentsInChildren<Collider>();
            foreach (var b in m_Bodies)
            {
                b.isKinematic = true;
            }
            foreach (var b in m_Colliders)
            {
                b.enabled = false;
            }
        }

        public void Activate()
        {
            foreach (var b in m_Bodies)
            {
                b.isKinematic = false;
            }
            foreach (var b in m_Colliders)
            {
                b.enabled = true;
            }
        }
    }
}
