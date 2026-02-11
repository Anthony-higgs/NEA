using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene to load when Play is pressed")]
    public string gameSceneName = "Level1";

    [Header("Scene to load when Armory is pressed")]
    public string gameSceneName1 = "Armory";

    [Header("Scene to load when Select level is pressed")]
    public string gameSceneName2 = "Levelselect";

    // Called by the Play button
    public void PlayGame()
    {
        int level = PlayerPrefs.GetInt("currentLevel", 1);
        Debug.Log("Playing Level " + level);

        SceneManager.LoadScene("Level" + level);
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
    //to reset all upgrades and gems
    public void ResetProgress()
    {
        // Reset UpgradeManager bools in memory
        if (UpgradeManager.Instance != null)
        {
            UpgradeManager.Instance.fuelTankUnlocked = false;
            UpgradeManager.Instance.ArmorUnlocked = false;
            UpgradeManager.Instance.TurretUnlocked = false;
        }

        // Reset PlayerPrefs so upgrades are not loaded next time
        PlayerPrefs.SetInt("FuelTankUnlocked", 0);
        PlayerPrefs.SetInt("ArmorUnlocked", 0);
        PlayerPrefs.SetInt("TurretUnlocked", 0);
        PlayerPrefs.SetInt("PlayerGems", 0);
        PlayerPrefs.DeleteAll();
        

        PlayerPrefs.Save();

        
    }
}