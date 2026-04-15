using System;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public GameObject start;
    public GameObject checkpoint;
    public bool hasHitCheckpoint;

    private CarControls controls;
    
    void Awake()
    {
        controls = new CarControls();
    }

    private void OnEnable()
    {
        controls.Car.Enable();
    }

    void Update()
    {
        if (controls.Car.Reset.triggered)
        {
            Debug.Log("Respawn");
            if (hasHitCheckpoint)
            {
                player.transform.position = checkpoint.transform.position;
                player.transform.rotation = checkpoint.transform.rotation;
            }
            else
            {
                player.transform.position = start.transform.position;
                player.transform.rotation = start.transform.rotation;
            }
        }
    }
}
