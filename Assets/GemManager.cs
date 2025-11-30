using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance;  

    [Header("Player Gems")]
    public int gems = 0;  // current gem count

    void Awake()
    {
        // ensure there’s only one GemManager and it stays across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep it when changing scenes
            LoadGems(); // load saved gems when game starts
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGems(int amount)
    {
        gems += amount;
        SaveGems();
        Debug.Log("Gems: " + gems);
    }

    public bool SpendGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            SaveGems();
            return true;
        }
        return false;
    }

    public void SaveGems()
    {
        PlayerPrefs.SetInt("PlayerGems", gems);
        PlayerPrefs.Save();
    }

    public void LoadGems()
    {
        gems = PlayerPrefs.GetInt("PlayerGems", 0);
    }
}