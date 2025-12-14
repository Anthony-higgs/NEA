using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string gameSceneName = "MainMenu";
    public void RestartLevel()
    {
        // Check if there's a saved level
        string lastLevel = PlayerPrefs.GetString("LastSelectedLevel", "Level1");
        SceneManager.LoadScene(lastLevel);
    }
    public void ToMenu()
    {
        
        SceneManager.LoadScene(gameSceneName);
    }
}
