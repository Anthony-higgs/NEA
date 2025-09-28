using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // only compiled in the Editor
#endif

public class MainMenu : MonoBehaviour
{
    [Header("Scene to load when Play is pressed")]
    public string gameSceneName = "GameScene"; // change to the exact name of your gameplay scene

    // Called by the Play button
    public void PlayGame()
    {
        // Load the gameplay scene by name
        SceneManager.LoadScene(gameSceneName);
    }

    // Called by the Quit button
    public void QuitGame()
    {
        // In builds this quits the application
        Application.Quit();

        // In the editor stop Play mode (so Quit does something while testing)
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}