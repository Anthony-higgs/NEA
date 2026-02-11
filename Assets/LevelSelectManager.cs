using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);

        // Extract number from the selected level.
        int levelNumber = int.Parse(levelName.Replace("Level", ""));
        PlayerPrefs.SetInt("currentLevel", levelNumber);
        PlayerPrefs.Save();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}