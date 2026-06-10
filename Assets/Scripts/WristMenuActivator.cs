using UnityEngine;

public class WristMenuActivator : MonoBehaviour
{
    [Tooltip("The radial menu GameObject")]
    public GameObject radialMenu;
    [Tooltip("Assign your XR Camera (head/camera) here")]
    public Transform cameraTransform;

    // Tweak these angles to control menu activation
    public float activationAngle = 40f; // degrees

    private void Update()
    {
        // Vector from watch to user's head
        Vector3 toHead = (cameraTransform.position - transform.position).normalized;
        // How much is the watch's forward vector pointing toward the head?
        float angle = Vector3.Angle(transform.forward, toHead);

        if (angle < activationAngle)
        {
            // Watch is facing toward the user's head: show menu
            if (!radialMenu.activeSelf)
                radialMenu.SetActive(true);
        }
        else
        {
            // Watch facing away: hide menu
            if (radialMenu.activeSelf)
                radialMenu.SetActive(false);
        }
    }
}
