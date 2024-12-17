using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyPlayer : CharacterControls
{
    public float healthAmount = 100;
    public float fallDamageThreshold = 10f;
    private float lastXPosition;
    private float lastYPosition;
    public Image healthBar;

    new void Awake()
    {
        base.Awake();  // Calls the Awake method from CharacterControls
        lastXPosition = transform.position.x;  // Track initial X position of the player
        lastYPosition = transform.position.y;  // Track initial Y position of the player
    }

    new void Update()
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
