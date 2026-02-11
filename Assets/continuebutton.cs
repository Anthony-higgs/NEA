using UnityEngine;
using UnityEngine.SceneManagement;

public class continuebutton : MonoBehaviour
{
    public void Continue()
    {
        //loads the next level 
        int level = PlayerPrefs.GetInt("currentLevel", 1);
        Debug.Log("Loading Level " + level);

        SceneManager.LoadScene("Level" + level);
    }
}
