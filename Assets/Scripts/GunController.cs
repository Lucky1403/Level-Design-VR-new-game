using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    [SerializeField] private LaserShoot laserShoot;
    [SerializeField] private InputActionProperty activateAction; 
    // Assign this in Inspector → XR Controller → Activate Usage

    private void Update()
    {
        if (activateAction.action.WasPressedThisFrame())
        {
            laserShoot.FireLaser();
        }
    }
}
