using UnityEngine;

namespace CubeRunner
{
    public class CubeCharacterController : MonoBehaviour
    {
        public float forwardSpeed = 1f;
        public float horizontalSpeed = 1f;

        [Range(0, 1)]
        public float xInput;

        private Rigidbody m_RB;
        private Transform m_Transform;

        private const float MAX_X = Constants.LEVEL_WIDTH / 2f;
        
        private void Awake()
        {
            m_RB = GetComponent<Rigidbody>();
            m_Transform = transform;
        }

        private void FixedUpdate()
        {
            var currentPosition = m_Transform.position;
            var vel = new Vector3();
            vel.x = xInput * horizontalSpeed;
            vel.z = forwardSpeed;
            m_RB.velocity = vel;

            var nextPos = currentPosition + Time.fixedDeltaTime * vel;

            if (Mathf.Abs(nextPos.x) >= MAX_X)
            {               
                nextPos.x = Mathf.Clamp(currentPosition.x, -MAX_X, MAX_X);
            }

            m_RB.MovePosition(nextPos);
        }
    }
}