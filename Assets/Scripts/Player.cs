using CandyCoded.HapticFeedback;
using UnityEngine;
using UnityEngine.Serialization;

namespace CubeRunner
{
    [RequireComponent(typeof(CubeHealthBar))]
    [RequireComponent(typeof(CubeCharacterController))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Animator m_AnimatedModel;

        [FormerlySerializedAs("m_ParticleSystem")]
        [SerializeField]
        private ParticleSystem m_ParticleSystem;

        [SerializeField]
        private ParticleSystem m_WarpEffect;

        [SerializeField]
        private BoxCollider m_TopBox;

        PlayerInput m_Input;

        private CubeCharacterController m_CharController;
        private CubeHealthBar m_HealthBar;
        private Vector3 m_StartModelPosition;

        private void Awake()
        {
            m_StartModelPosition = m_TopBox.transform.localPosition;

            m_Input = new PlayerInput();
            m_Input.Enable();

            m_CharController = GetComponent<CubeCharacterController>();
            m_CharController.onActivated += () =>
            {
                m_WarpEffect.Play();
            };  

            m_HealthBar = GetComponent<CubeHealthBar>();
            m_HealthBar.onDeath += ExecuteDeath;
            m_HealthBar.onCubeAdded += M_HealthBar_onCubeAdded;
        }

        private void Update()
        {
            UpdateInput();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IVisitor<CubeHealthBar>>(out var visitor))
            {
                visitor.Visit(m_HealthBar);
            }
        }

        private void M_HealthBar_onCubeAdded()
        {
            m_AnimatedModel.SetTrigger("Jump");
            m_ParticleSystem?.Play();

            UpdateTopBox(m_HealthBar.GetHeight());
        }

        private void UpdateTopBox(float cubesHeight)
        {
            var modelPos = m_StartModelPosition;
            modelPos.y += cubesHeight;
            m_TopBox.transform.localPosition = modelPos;
        }

        public void ExecuteDeath()
        {          
            HapticFeedback.HeavyFeedback();
            m_CharController.enabled = false;
            ActivateRagdoll();
        }

        private void ActivateRagdoll()
        {
            m_AnimatedModel.enabled = false;
            m_AnimatedModel.transform.SetParent(null);
            m_AnimatedModel.GetComponent<Ragdoll>().Activate();
            m_CharController.Collider.enabled = false;
            m_TopBox.enabled = false;
        }

        private void UpdateInput()
        {
            var input = m_Input.Gameplay.ScreenPosition.ReadValue<Vector2>();
            float xInput = input.x;
            m_CharController.inputScreenX = xInput;
        }
    }
}