using UnityEngine;
using TMPro;

public class GemUI : MonoBehaviour
{
    public TextMeshProUGUI gemText;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (GemManager.Instance != null)
            gemText.text = "Gems:" + GemManager.Instance.gems.ToString();
    }
}