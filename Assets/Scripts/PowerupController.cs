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
            
            Debug.Log("Powerup picked up");
        }
    }

    // TODO: On collision with building and hasPowerup => building breaks
    // else car breaks + player loses
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Building") && hasPowerup) { }
        else { }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupRoutine = null;
    }
}
