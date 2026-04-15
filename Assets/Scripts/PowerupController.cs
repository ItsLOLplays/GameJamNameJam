using System;
using System.Collections;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public bool hasPowerup;
    private Coroutine powerupRoutine;
    public TimerController timerController;
    public FractureTruck fractureTruck;
    public GameObject winUI;
    public GameObject loseUI;
    
    private bool hasWon;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            if (powerupRoutine != null)
            {
                StopCoroutine(powerupRoutine);
            }
            
            timerController.StartTimer(80f);
            
            powerupRoutine = StartCoroutine(PowerupCountdownRoutine());

            other.gameObject.SetActive(false);
        }
        
        if (other.gameObject.CompareTag("Ending") || other.gameObject.CompareTag("Checkpoint"))
        {

            Fracture fracture = other.gameObject.GetComponent<Fracture>();

            if (hasPowerup)
            {
                if (fracture != null)
                {
                    fracture.Explode();
                }

                if (other.gameObject.CompareTag("Ending"))
                {
                    Win();
                }
            }
            else
            {
                if (!hasWon)
                {
                    Lose();
                }
            }
        }
    }

    void Lose()
    {
        fractureTruck.Explode();
        loseUI.SetActive(true);
    }

    void Win()
    {
        Debug.Log("Win");
        winUI.SetActive(true);
        hasWon = true;
    }
    
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(80);
        hasPowerup = false;
        powerupRoutine = null;
    }
}
