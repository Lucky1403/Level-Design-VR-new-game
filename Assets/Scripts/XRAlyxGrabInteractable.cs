using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRAlyxGrabInteractable : XRGrabInteractable
{
    public float VelocityThreshold = 2;
    public float JumpAngleInDegree = 60;
    private XRRayInteractor rayInteractor;
    private Vector3 previousPosition;

    private Rigidbody interactableRigidbody;
    private bool canJump = true;

    protected override void Awake()
    {
        base.Awake();
        interactableRigidbody = GetComponent<Rigidbody>();
        canJump = false;
    }

    private void Update()
    {
        if (isSelected && firstInteractorSelecting is XRRayInteractor)
        {
            Vector3 velocity = (rayInteractor.transform.position - previousPosition) / Time.deltaTime;
            previousPosition = rayInteractor.transform.position;

            if (velocity.magnitude > VelocityThreshold)
            {
                Drop();
                interactableRigidbody.velocity = ComputeVelocity();
                canJump = false;
            }
        }
    }

    public Vector3 ComputeVelocity()
    {
        Vector3 diff = rayInteractor.transform.position - transform.position;
        Vector3 diffXZ = new Vector3(diff.x, 0, diff.z);
        float diffXZLength = diffXZ.magnitude;
        float diffYLength = diff.y;

        float AngleInRadian = JumpAngleInDegree * Mathf.Deg2Rad;

        float jumpSpeed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(diffXZLength, 2) / (2 * Mathf.Cos(AngleInRadian) * Mathf.Cos(AngleInRadian) * (diffXZ.magnitude * Mathf.Tan(AngleInRadian) - diffYLength)));

        Vector3 jumpVelocityVector = diffXZ.normalized * Mathf.Cos(AngleInRadian) * jumpSpeed + Vector3.up * Mathf.Sin(AngleInRadian) * jumpSpeed;

        return jumpVelocityVector;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRRayInteractor)
        {
            trackPosition = false;
            trackRotation = false;
            throwOnDetach = false;

            rayInteractor = (XRRayInteractor)args.interactorObject;
            previousPosition = rayInteractor.transform.position;
            canJump = true;
        }
        else
        {
            trackPosition = true;
            trackRotation = true;
            throwOnDetach = true;
        }
        base.OnSelectEntered(args);
    }
}
 