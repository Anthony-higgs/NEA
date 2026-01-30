using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public int nextLevelIndex = 2; // Level to go to after this one
    public string levelCompleteScene = "LevelComplete";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("LEVEL COMPLETE!");

        // Save next level
        PlayerPrefs.SetInt("currentLevel", nextLevelIndex);
        PlayerPrefs.Save();

        // Load Level Complete screen
        SceneManager.LoadScene(levelCompleteScene);
    }
}