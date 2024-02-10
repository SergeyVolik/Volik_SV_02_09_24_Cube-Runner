using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CubeRunner
{
    public class GameUI : MonoBehaviour
    {

        [SerializeField]
        private Button m_TryAgainButton;

        [SerializeField]
        private RectTransform m_StartUI;

        [SerializeField]
        private RectTransform m_DeathUI;

        private RectTransform m_PrevUI;
        private PlayerInput m_Input;
        private Player m_Player;

        private void Awake()
        {
            m_Input = new PlayerInput();

            m_Input.Gameplay.ScreenPosition.performed += Move_performed;
            m_Input.Enable();
            m_Player = FindObjectOfType<Player>();
            var healthBar = m_Player.GetComponent<CubeHealthBar>();
            Assert.IsTrue(m_Player != null);

            healthBar.onDeath += Player_onDeath;

            m_Player.GetComponent<CubeCharacterController>().SetEnableController(false);

            m_TryAgainButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });


            DeactivateAllUI();
            ActivateUI(m_StartUI);
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_Input.Gameplay.ScreenPosition.performed -= Move_performed;

            m_Player.GetComponent<CubeCharacterController>().SetEnableController(true);
            m_StartUI.gameObject.SetActive(false);
        }

        private void Player_onDeath()
        {
            ActivateUI(m_DeathUI);
        }

        private IEnumerable<GameObject> GetAllUI()
        {
            yield return m_StartUI.gameObject;
            yield return m_DeathUI.gameObject;
        }
        void DeactivateAllUI()
        {
            foreach (GameObject go in GetAllUI())
            {
                go.SetActive(false);
            }
        }

        public void ActivateUI(RectTransform ui)
        {
            if (m_PrevUI)
            {
                m_PrevUI.gameObject.SetActive(false);
            }
            m_PrevUI = ui;
            ui.gameObject.SetActive(true);
        }
    }
}