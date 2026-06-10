using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandProximityActivator : MonoBehaviour
{
    [Tooltip("Radial Menu GameObject to toggle")]
    public GameObject radialMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            radialMenu.SetActive(true); // Show radial menu
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            radialMenu.SetActive(false); // Hide radial menu
        }
    }
}
