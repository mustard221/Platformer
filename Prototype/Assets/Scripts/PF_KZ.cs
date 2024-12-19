using UnityEngine;

public class KZTriggerPlatform : MonoBehaviour
{
    public GameObject killZone;  
    public bool isActivated = false;  // check if KillZone is activated

    private void Start()
    {
       
        if (killZone != null)
        {
            killZone.SetActive(false);  // deactivate killzone initially
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player") && !isActivated)
        {
      
            if (gameObject.name == "Platforms (8)")
            {
                ActivateKillZone();  // activate the Killzone when land on platform
            }
        }
    }

    private void ActivateKillZone()
    {
        // check if KillZone is assigned
        if (killZone != null)
        {
            
            if (!killZone.activeSelf)
            {
                killZone.SetActive(true);  
                isActivated = true;  // killzone is activated
            }
        }
    }
}
