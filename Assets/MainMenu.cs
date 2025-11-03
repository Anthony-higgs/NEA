using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Armory button
    public void Armory()
    {
        SceneManager.LoadScene(gameSceneName1);
    }

    // Level select button
    public void LevelSelect()
    {
        SceneManager.LoadScene(gameSceneName2);
    }

    // Quit button
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}