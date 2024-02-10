using UnityEngine;

namespace CubeRunner
{
    public class CameraTargetFixer : MonoBehaviour
    {
        private Transform m_Transform;

        private void Awake()
        {
            m_Transform = transform;
        }

        private void Update()
        {
            var pos = m_Transform.position;
            pos.x = 0;
            m_Transform.position = pos;
        }
    }
}