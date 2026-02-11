using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text speedText;
    public TMP_Text fuelText;
    public TMP_Text gemsText;

    public PlayerHealth playerHealth;
    public PlayerMovement2D playerMovement;
    public PlayerFuel playerFuel;
    

    private void Update()
    {
        if (playerHealth != null)
            healthText.text = "Health: " + playerHealth.currentHealth;

        if (playerMovement != null)
            speedText.text = "Speed: " + Mathf.Round(playerMovement.currentSpeed);

        if (playerFuel != null)
            fuelText.text = "Fuel: " + Mathf.Round(playerFuel.currentFuel);

    }
}