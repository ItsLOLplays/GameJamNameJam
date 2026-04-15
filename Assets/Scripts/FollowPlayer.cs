using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smoothing = 6f;
    public Transform lookAtTarget;
    public Transform positionTarget;
    private bool isLocked;
    
    void FixedUpdate()
    {
        if (isLocked) return;
        
        transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);
        transform.LookAt(lookAtTarget);
    }
    
    public void LockCamera()
    {
        isLocked = true;
    }

    public void UnlockCamera()
    {
        isLocked = false;
    }
}