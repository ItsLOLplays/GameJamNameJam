using System;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private float timeLeft;
    
    public TMP_Text timerText;
    public TMP_Text otherText;

    private void Start()
    {
        timerText.gameObject.SetActive(false);
        otherText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("0.00");
        }
        else
        {
            timerText.gameObject.SetActive(false);
            otherText.gameObject.SetActive(false);
        }
    }

    public void StartTimer(float startTime)
    {
        timeLeft = startTime;
        timerText.gameObject.SetActive(true);
        otherText.gameObject.SetActive(true);
    }
}
