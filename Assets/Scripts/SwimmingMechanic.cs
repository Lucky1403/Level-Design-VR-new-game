using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]


public class SwimmingMechanic : MonoBehaviour
{
    [Header("Swimming Mechanic Settings")]

    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1f;
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;


    [Header("References")]
    [SerializeField] InputActionReference leftControllerSwimReference;
    [SerializeField] InputActionReference leftControllerVeclocity;
    [SerializeField] InputActionReference rightControllerSwimReference;
    [SerializeField] InputActionReference rightControllerVeclocity;
    [SerializeField] Transform trackingReference;

    Rigidbody _rigidbody;
    float _cooldowntime;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        _cooldowntime += Time.fixedDeltaTime;
        if(_cooldowntime > minTimeBetweenStrokes && leftControllerSwimReference.action.IsPressed() && rightControllerSwimReference.action.IsPressed())
        {
            var leftHandVelocity = leftControllerVeclocity.action.ReadValue<Vector3>();
            var rightHandVelocity = rightControllerVeclocity.action.ReadValue<Vector3>();

            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;

            if(localVelocity.sqrMagnitude > (minForce * minForce))
            {
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldowntime = 0f;
            }

            if(_rigidbody.velocity.sqrMagnitude > 0.01f)
            {
                _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
            }
            
        }
        
    }



}
