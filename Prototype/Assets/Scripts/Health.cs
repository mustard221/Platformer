using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float healthAmount = 100;
    public float fallDamageAmount = 10f;
    private float lastYPosition;
    public Image healthBar;  

    private void Start()
    {
        lastYPosition = transform.position.y;  // track y position
        UpdateHealthBar();  // update healthbar accordingly
    }

    private void Update()
    {
        if (transform.position.y < lastYPosition)  // player falling y position lowering
        {
            float fallDistance = lastYPosition - transform.position.y;  // getting distance fallen
            if (fallDistance >= fallDamageAmount)
            {
                TakeFallDamage(fallDistance);  // get fall damage using fall distance
            }
        }

        // if health is 0 or less, respawn or reload scene
        if (healthAmount <= 0)
        {
            RespawnPlayer();
        }

        // update y position
        lastYPosition = transform.position.y;

        // update the health bar every frame
        UpdateHealthBar();
    }

    private void TakeFallDamage(float fallDistance)  // apply fall damage
    {
        healthAmount -= fallDistance * 0.5f;
    }

    private void RespawnPlayer()
    {
        Vector3 checkpoint = GetComponent<CharacterControls>().checkPoint;

        if (checkpoint != Vector3.zero)  // if checkpoint exists respawn
        {
            transform.position = checkpoint;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // reload scene if not
        }

        healthAmount = 100;  // reset health after spawn
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;  // set the healthbar based on health
        }
    }
}
