using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string menuScene = "MainMenu";

    public void RestartLevel()
    {
        int level = PlayerPrefs.GetInt("currentLevel", 1);
        Debug.Log("Restarting Level " + level);

        SceneManager.LoadScene("Level" + level);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}