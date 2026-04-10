using System.Collections;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public bool hasPowerup;
    private Coroutine powerupRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            if (powerupRoutine != null)
            {
                StopCoroutine(powerupRoutine);
            }
            
            powerupRoutine = StartCoroutine(PowerupCountdownRoutine());

            other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject); 
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            Fracture fracture = collision.gameObject.GetComponent<Fracture>();

            if (hasPowerup)
            {
                if (fracture != null)
                {
                    fracture.Explode();
                }
            }
            else
            {
                // Loss();
            }
        }
    }

    // TODO: player loses => Loss screen
    // void Loss() { }
    
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupRoutine = null;
    }
}
