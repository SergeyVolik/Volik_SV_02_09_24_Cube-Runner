using UnityEngine;

namespace CubeRunner
{
    public class CameraTarget : MonoBehaviour
    {
        private Transform m_Transform;

        private Transform m_PlayerTransform;

        private void Awake()
        {
            m_Transform = transform;        
        }

        private void Update()
        {
            if (m_PlayerTransform == null)
            {
                var player = FindAnyObjectByType<Player>();

                if (player == null)
                    return;

                m_PlayerTransform = player.transform;
            }

            var pos = m_PlayerTransform.position;
            pos.x = 0;
            m_Transform.position = pos;
        }
    }
}