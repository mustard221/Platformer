using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : CharacterControls
{
    public float healthAmount = 100;
    public float fallDamageThreshold = 10f;  // Distance after which fall damage is applied
    private float lastXPosition;
    private float lastYPosition;
    public Image healthBar;

    // This method now calls the base class Start method
    new void Start()
    {
        base.Start();  // Calls the Start method from CharacterControls for initialization
        lastXPosition = transform.position.x;  // Track initial X position of the player
        lastYPosition = transform.position.y;  // Track initial Y position of the player
        UpdateHealthBar();  // Update health bar accordingly
    }

    void Update()  // Removed the 'new' keyword
    {
        // Calculate fall distance only when the player is falling (checking the Y axis)
        if (!IsGrounded())  // Player is in the air
        {
            if (transform.position.y < lastYPosition)  // Only calculate fall distance if falling
            {
                float fallDistanceY = lastYPosition - transform.position.y;  // Vertical fall distance

                // If the player is falling a significant distance vertically, apply damage
                if (fallDistanceY >= fallDamageThreshold)
                {
                    TakeFallDamage(fallDistanceY);  // Apply fall damage based on Y distance
                }
            }
        }

        // Update health bar every frame
        UpdateHealthBar();

        // Update the last X and Y position at the end of each frame
        lastXPosition = transform.position.x;
        lastYPosition = transform.position.y;

        base.Update();  // Calls the Update method from CharacterControls
    }

    private void TakeFallDamage(float fallDistanceY)
    {
        // Apply fall damage as a fraction of the fall distance
        healthAmount -= fallDistanceY * 0.5f;  // Adjust the fall damage formula as needed
    }

    private void RespawnPlayer()
    {
        Vector3 checkpoint = GetComponent<CharacterControls>().checkPoint;

        if (checkpoint != Vector3.zero)  // If a checkpoint exists, respawn at it
        {
            transform.position = checkpoint;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload scene if no checkpoint
        }

        healthAmount = 100;  // Reset health after respawn
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;  // Set health bar fill amount based on health
        }
    }
}
