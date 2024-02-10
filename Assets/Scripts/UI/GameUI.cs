using CubeRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] 
    private Button m_StartButton;

    [SerializeField] 
    private Button m_TryAgainButton;

    [SerializeField]
    private RectTransform m_StartUI;

    [SerializeField]
    private RectTransform m_DeathUI;

    private RectTransform m_PrevUI;
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

    private void Awake()
    {

        var player = FindObjectOfType<Player>();
        Assert.IsTrue(player != null);

        player.onDeath += Player_onDeath;

        player.GetComponent<CubeCharacterController>().enabled = false;
     
        m_StartButton.onClick.AddListener(() => {
            player.GetComponent<CubeCharacterController>().enabled = true;
            m_StartUI.gameObject.SetActive(false);
        });

        m_TryAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });


        DeactivateAllUI();
        ActivateUI(m_StartUI);
    }

    private void Player_onDeath()
    {
        ActivateUI(m_DeathUI);
    }
}
