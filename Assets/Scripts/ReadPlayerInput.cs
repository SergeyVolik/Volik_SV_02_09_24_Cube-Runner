using UnityEngine;

namespace CubeRunner
{
    public class ReadPlayerInput : MonoBehaviour
    {
        private PlayerInput m_Input;
        private CubeCharacterController m_CharController;

        private void Awake()
        {
            m_Input = new PlayerInput();
            m_Input.Enable();
            m_CharController = GetComponent<CubeCharacterController>();
        }
        private void Update()
        {
            UpdateInput();
        }

        private void UpdateInput()
        {
            var input = m_Input.Gameplay.ScreenPosition.ReadValue<Vector2>();
            float xInput = input.x;
            m_CharController.inputScreenX = xInput;
        }
    }
}