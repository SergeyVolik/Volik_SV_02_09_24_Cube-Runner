using CandyCoded.HapticFeedback;
using UnityEngine;
using UnityEngine.Serialization;

namespace CubeRunner
{
    [RequireComponent(typeof(CubeHealthBar))]
    [RequireComponent(typeof(CubeCharacterController))]
    public class CubeCharacter : MonoBehaviour
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

        public BoxCollider CharacterBox => m_TopBox;

        private CubeCharacterController m_CharController;
        public CubeHealthBar HealthBar { get; private set; }
        private Vector3 m_StartModelPosition;

        public bool IsDead { get; private set; }
        private void Awake()
        {
            m_StartModelPosition = m_TopBox.transform.localPosition;

            m_CharController = GetComponent<CubeCharacterController>();
            m_CharController.onActivated += () =>
            {
                m_WarpEffect.Play();
            };

            HealthBar = GetComponent<CubeHealthBar>();
            HealthBar.onDeath += ExecuteDeath;
            HealthBar.onCubeAdded += M_HealthBar_onCubeAdded;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IVisitor<CubeCharacter>>(out var visitor))
            {
                visitor.Visit(this);
            }
        }

        private void M_HealthBar_onCubeAdded()
        {
            m_AnimatedModel.SetTrigger("Jump");
            m_ParticleSystem?.Play();

            UpdateTopBox(HealthBar.GetHeight());
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
            IsDead = true;
        }

        private void ActivateRagdoll()
        {
            m_AnimatedModel.enabled = false;
            m_AnimatedModel.transform.SetParent(null);
            m_AnimatedModel.GetComponent<Ragdoll>().Activate();
            m_CharController.Collider.enabled = false;
            m_TopBox.enabled = false;
        }

    }
}