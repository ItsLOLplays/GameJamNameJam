using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FractureTruck : MonoBehaviour
{
    public GameObject fractureObject;
    public GameObject originalObject;
    public GameObject wheels;
    public CarController carController;
    public float explosionMinForce = 5f;
    public float explosionMaxForce = 100f;
    public float explosionForceRadius = 10f;
    public float fragScaleFactor = 1f;
    public FollowPlayer camera;
    
    private bool hasExploded;
    private bool hasPlayedBreak;
    
    private void Start()
    {
        if (fractureObject != null)
        {
            fractureObject.SetActive(false);
        }
    }
    
    public void Explode()
    {
        if (originalObject != null && !hasExploded)
        {
            wheels.SetActive(false);
            hasExploded = true;
            
            camera.LockCamera();
            
            originalObject.SetActive(false);
            
            carController.carEngineSound.Stop();
            carController.tireScreechSound.mute = true;

            if (fractureObject != null)
            {
                fractureObject.SetActive(true);
                fractureObject.transform.position = originalObject.transform.position;

                foreach (Transform t in fractureObject.transform)
                {
                    var rb = t.GetComponent<Rigidbody>();
                    
                    if (rb != null)
                    {
                        Random.InitState(DateTime.Now.Millisecond);
                        rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce),
                            originalObject.transform.position, explosionForceRadius);
                    }
                }
            }
            hasExploded = false;
        }
    }
}
