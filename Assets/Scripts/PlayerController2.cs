using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController2 : MonoBehaviour
{
    private float maxAngle = 60f;
    private float maxTorque = 600f;
    private float maxSpeed = 50f;
    private float turnSpeed = 2f;
    
    private float angle;
    private float torque;
    
    private Rigidbody rb;
    
    public InputActionAsset primaryActions;
    private InputActionMap playerActionMap;
    private InputAction turningInputAction;
    private InputAction movementInputAction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.linearDamping = 0.5f;
        rb.angularDamping = 1f;
    }
    
    void Awake()
    {
        playerActionMap = primaryActions.FindActionMap("Player");
        
        movementInputAction = playerActionMap.FindAction("Movement");
        turningInputAction = playerActionMap.FindAction("Turning");
        
        movementInputAction.performed += GetTorqueInput;
        movementInputAction.canceled += GetTorqueInput;
        
        turningInputAction.performed += GetAngleInput;
        turningInputAction.canceled += GetAngleInput;
    }
    
    private void GetTorqueInput(InputAction.CallbackContext context)
    {
        torque = context.ReadValue<float>();
    }

    private void GetAngleInput(InputAction.CallbackContext context)
    {
        angle = context.ReadValue<float>();
    }
    
    private void OnEnable()
    {
        turningInputAction.Enable();
        movementInputAction.Enable();
    }
    
    private void OnDisable()
    {
        turningInputAction.Disable();
        movementInputAction.Disable();
    }
    
    void FixedUpdate()
    {
        Vector3 forward = rb.rotation * Vector3.forward;
        forward.y = 0f;
        forward.Normalize();
        
        Movement(forward);
        Rotation(forward);
    }

    private void Movement(Vector3 forward)
    {
        float speed = rb.linearVelocity.magnitude;

        float speedFactor = 1f - (speed / maxSpeed);
        speedFactor = Mathf.Clamp01(speedFactor);

        Vector3 currentSpeed = forward * (torque * maxTorque * speedFactor);
        
        rb.AddForce(currentSpeed, ForceMode.Acceleration);
    }
    
    private void Rotation(Vector3 forward)
    {
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, forward);
        
        float direction = 1f;
        if (forwardSpeed == 0f) direction = 0f;
        else if (forwardSpeed < -0.001f) direction = -1f;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, angle * maxAngle * Time.fixedDeltaTime * turnSpeed * direction, 0f));
    }
}