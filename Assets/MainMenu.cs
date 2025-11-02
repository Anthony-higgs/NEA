using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; 
#endif

public class MainMenu : MonoBehaviour
{
    [Header("Scene to load when Play is pressed")]
    public string gameSceneName = "SampleScene";
    [Header("Scene to load when Armory is pressed")]
    public string gameSceneName1 = "Armory";
    [Header("Scene to load when Select level is pressed")]
    public string gameSceneName2 = "Levelselect";

    // Called by the Play button
    public void PlayGame()
    {
        // Check if there's a saved level
        string lastLevel = PlayerPrefs.GetString("LastSelectedLevel", "Level1");

        // Load that level (default to Level1 if none saved)
        SceneManager.LoadScene(lastLevel);
    }
    // Called by the Armory button
    public void Armory()
    {
        // Load the gameplay scene by name
        SceneManager.LoadScene(gameSceneName1);
    }
    // Called by the Levelselect button
    public void Levelselect()
    {
        // Load the gameplay scene by name
        SceneManager.LoadScene(gameSceneName2);
    }
    // Called by the Quit button
    public void QuitGame()
    {
        // In builds this quits the application
        Application.Quit();

        // In the editor stop play mode (so Quit does something while testing)
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
