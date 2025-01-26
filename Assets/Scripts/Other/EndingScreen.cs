using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)){
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
         #else
              Application.Quit();
         #endif
        }
        else if (Input.GetKeyUp(KeyCode.Return)){
            SceneManager.LoadScene("Game");
        }
    }
}
