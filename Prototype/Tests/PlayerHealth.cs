using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float healthAmount = 100;
    public float fallDamageThreshold = 10f;  // Distance after which fall damage is applied
    private float lastXPosition;
    private float lastYPosition;
    public Image healthBar;

    private void Start()
    {
        lastXPosition = transform.position.x;  // Track initial X position of the player
        lastYPosition = transform.position.y;  // Track initial Y position of the player
        UpdateHealthBar();  // Update health bar accordingly
    }

    private void Update()
    {
        // Calculate fall distance only when the player is falling (checking the Y axis)
        if (transform.position.y < lastYPosition)  // Player is falling
        {
            // Calculate fall distance in both X and Y directions
            float fallDistanceX = lastXPosition - transform.position.x;  // Horizontal movement
            float fallDistanceY = lastYPosition - transform.position.y;  // Vertical fall distance

            // If the player is falling a significant distance vertically, apply damage
            if (fallDistanceY >= fallDamageThreshold)
            {
                TakeFallDamage(fallDistanceY, fallDistanceX);  // Apply fall damage based on Y distance, consider X movement too
            }
        }

        // Check if health is 0 or less, respawn or reload scene
        if (healthAmount <= 0)
        {
            RespawnPlayer();
        }

        // Update the last X and Y position at the end of each frame
        lastXPosition = transform.position.x;
        lastYPosition = transform.position.y;

        // Update the health bar every frame
        UpdateHealthBar();
    }

    private void TakeFallDamage(float fallDistanceY, float fallDistanceX)
    {
        // Apply fall damage as a fraction of the fall distance, considering both Y and X distance
        healthAmount -= fallDistanceY * 0.5f + Mathf.Abs(fallDistanceX) * 0.1f;  // Adjust the fall damage formula as needed
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

    // Use OnCollisionEnter or OnTriggerEnter to detect when player touches the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // Check if the object hit is tagged "Ground"
        {
            // Reset fall distance when the player hits the ground
            lastXPosition = transform.position.x;
            lastYPosition = transform.position.y;
        }
    }
}
