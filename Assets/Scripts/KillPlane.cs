using System;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    public FractureTruck fracture;
    public GameObject LossUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LossUI.SetActive(true);
            fracture.Explode();
        }
    }
}
