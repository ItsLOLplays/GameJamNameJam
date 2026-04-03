using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController2 : MonoBehaviour
{
    public float maxAngle = 60f;
    public float maxTorque = 60f;
    public float maxSpeed = 15f;
    
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
        Debug.Log("Torque: " + torque);
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

        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(forward * (torque * maxTorque), ForceMode.Acceleration);
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, angle * maxAngle * Time.fixedDeltaTime, 0f));
    }
}