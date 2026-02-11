using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public string levelCompleteScene = "LevelComplete";
    public int lastLevel = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);

        // If not last level, go to next
        if (currentLevel < lastLevel)
        {
            PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        }
        else
        {
            // Reset after final level
            PlayerPrefs.SetInt("currentLevel", 1);
        }

        PlayerPrefs.Save();

        Debug.Log("Level complete. Next level = " + PlayerPrefs.GetInt("currentLevel"));

        SceneManager.LoadScene(levelCompleteScene);
    }
}