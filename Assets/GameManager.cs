using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // "singleton" so i can access it anywhere

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep between scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Stopping everything...");
        Time.timeScale = 0f; // freezes the game

        // Here i could load a Game Over screen, or restart
        // SceneManager.LoadScene("GameOverScene");
    }
}
