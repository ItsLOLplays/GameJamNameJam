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
        
        Debug.Log("Hit: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Building"))
        {
            Debug.Log("Hit building!");

            Fracture fracture = other.gameObject.GetComponent<Fracture>();

            if (hasPowerup)
            {
                Debug.Log("Has powerup!");

                if (fracture != null)
                {
                    Debug.Log("Exploding!");
                    fracture.Explode();
                }
            }
            else
            {
                Debug.Log("Something went wrong?");
                // Loss();
            }
        }
    }
    
    // private void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Hit: " + collision.gameObject.name);
    //     
    //     if (collision.gameObject.CompareTag("Building"))
    //     {
    //         Debug.Log("Hit building!");
    //         
    //         Fracture fracture = collision.gameObject.GetComponent<Fracture>();
    //
    //         if (hasPowerup)
    //         {
    //             Debug.Log("Has powerup!");
    //             
    //             if (fracture != null)
    //             {
    //                 Debug.Log("Exploding!");
    //                 fracture.Explode();
    //             }
    //         }
    //         else
    //         {
    //             Debug.Log("Something went wrong?");
    //             // Loss();
    //         }
    //     }
    // }

    // TODO: player loses => Loss screen
    // void Loss() { }
    
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupRoutine = null;
    }
}
