using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallDamageThreshold = 10f;  // Distance threshold for fall damage
    public float damageMultiplier = 0.5f;  // Multiplier for fall damage based on distance

    private GameObject player;  // Reference to the player object
    private float lastYPosition;  // Store last Y position to check if falling
    private float health;  // Player's health

    private void Start()
    {
        // Find the player by tag and initialize the health
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            health = player.GetComponent<PlayerHealth>().health;  // Assuming PlayerHealth script manages health
            lastYPosition = player.transform.position.y;  // Initialize last Y position
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Check if the player is falling
            if (player.transform.position.y < lastYPosition)
            {
                float fallDistance = lastYPosition - player.transform.position.y;  // Calculate fall distance

                if (fallDistance >= fallDamageThreshold)
                {
                    TakeFallDamage(fallDistance);  // Apply fall damage
                }
            }

            // Update the last Y position
            lastYPosition = player.transform.position.y;
        }
    }

    private void TakeFallDamage(float fallDistance)
    {
        float fallDamage = (fallDistance - fallDamageThreshold) * damageMultiplier;  // Calculate the damage
        health -= fallDamage;  // Apply the damage to health

        // You can handle the health update in your PlayerHealth script
        player.GetComponent<PlayerHealth>().health = health;

        if (health <= 0)
        {
            Die();  // Handle player death if health reaches 0
        }
    }

    private void Die()
    {
        // You can add a respawn or game over logic here
        Debug.Log("Player has died due to fall damage.");
    }
}
