using UnityEngine;
using UnityEngine.SceneManagement;

public class continuebutton : MonoBehaviour
{
    public void Continue()
    {
        int level = PlayerPrefs.GetInt("currentLevel", 1);
        Debug.Log("Loading Level " + level);

        SceneManager.LoadScene("Level" + level);
    }
}
