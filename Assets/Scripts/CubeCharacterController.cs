using System;
using Unity.Mathematics;
using UnityEngine;

namespace CubeRunner
{
    public class CubeCharacterController : MonoBehaviour
    {
        public float forwardSpeed = 1f;
        public float horizontalSpeed = 1f;

        public float inputScreenX;

        private Rigidbody m_RB;
        public Collider Collider { get; private set; }
        private Transform m_Transform;

        private const float MAX_X = Constants.LEVEL_WIDTH / 2f;
        
        public event Action onActivated = delegate { };

        public void SetEnableController(bool enable)
        {
            enabled = enable;

            if (enable == true)
            {
                onActivated.Invoke();
            }
        }

        private void Awake()
        {
            m_RB = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            m_Transform = transform;
        }

        private void FixedUpdate()
        {
            var currentPosition = m_Transform.position;
            var vel = new Vector3();
            vel.z = forwardSpeed;
            m_RB.velocity = vel;

            float x = math.remap(0, Screen.width, -MAX_X, MAX_X, inputScreenX); 
            var nextPos = currentPosition + Time.fixedDeltaTime * vel;
            nextPos.x = Mathf.Clamp(x, -MAX_X, MAX_X);

            m_RB.MovePosition(nextPos);
        }
    }
}