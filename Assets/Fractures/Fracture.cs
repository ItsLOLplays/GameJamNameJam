using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fracture : MonoBehaviour
{
    public GameObject fractureObject;
    public GameObject originalObject;
    public float explosionMinForce = 5f;
    public float explosionMaxForce = 100f;
    public float explosionForceRadius = 10f;
    public float fragScaleFactor = 1f;
    public AudioSource breakSound;
    public AudioSource debrisSound;
    public CarController respawn;

    private bool hasExploded;
    private bool hasPlayedBreak;
    private bool hasPlayedDebris;

    private void Start()
    {
        if (fractureObject != null)
        {
            fractureObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasExploded)
        {
            respawn.hasHitCheckpoint = true;
        }
    }

    public void Explode()
    {
        if (originalObject != null && !hasExploded)
        {
            hasExploded = true;
            
            originalObject.SetActive(false);
            breakSound.Play();
            debrisSound.PlayDelayed(breakSound.clip.length / 2f);

            if (fractureObject != null)
            {
                fractureObject.SetActive(true);

                foreach (Transform t in fractureObject.transform)
                {
                    var rb = t.GetComponent<Rigidbody>();
                    
                    if (rb != null)
                    {
                        Random.InitState(DateTime.Now.Millisecond);
                        rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce),
                            originalObject.transform.position, explosionForceRadius);
                    }

                    StartCoroutine(Shrink(t, 6f));
                }

                StopCoroutine(nameof(Shrink));
                Destroy(fractureObject, 15f);
            }
        }
    }

    IEnumerator Shrink(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 newScale = t.localScale;

        while (newScale.x >= 0)
        {
            newScale -= new Vector3(fragScaleFactor, fragScaleFactor, fragScaleFactor);

            t.localScale = newScale;
            yield return new WaitForSeconds(0.05f);
        }
    }
}