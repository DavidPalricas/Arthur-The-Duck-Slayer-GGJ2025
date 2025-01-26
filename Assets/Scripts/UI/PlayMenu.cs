using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    public void playGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }
}

