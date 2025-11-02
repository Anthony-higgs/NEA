using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        // Save the selected level name 
        PlayerPrefs.SetString("LastSelectedLevel", levelName);
        PlayerPrefs.Save(); // make sure it’s written to disk

        // Load that level immediately
        SceneManager.LoadScene(levelName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}