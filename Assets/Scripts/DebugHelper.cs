using UnityEngine;
using UnityEngine.SceneManagement;

namespace CubeRunner
{
    public class DebugHelper : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
