using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smoothing = 6f;
    public Transform lookAtTarget;
    public Transform positionTarget;
    
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);
        transform.LookAt(lookAtTarget);
    }
}