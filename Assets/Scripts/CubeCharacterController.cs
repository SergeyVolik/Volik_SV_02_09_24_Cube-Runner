using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeCharacterController : MonoBehaviour
{
    public float forwardSpeed = 1f;
    public float horizontalSpeed = 1f;

    [Range(0, 1)]
    public float xInput;

    private Rigidbody m_RB;
    private Transform m_Transform;
    [SerializeField]
    private float m_MaxXOffset;

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

        if (Mathf.Abs(nextPos.x) >= m_MaxXOffset)
        {
            nextPos.x = currentPosition.x;
        }

        m_RB.MovePosition(nextPos);
    }
}