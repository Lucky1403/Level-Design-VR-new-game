using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandSwitcher : MonoBehaviour
{
    public RadialSelection radialSelection; // Assign in the Inspector

    public GameObject normalLeftHand;
    public GameObject normalRightHand;
    public GameObject rayInteractorLeftHand;
    public GameObject rayInteractorRightHand;
    public GameObject detectiveLensLeftHand;
    public GameObject gunRightHand;

    // Add these variables and assign them in the Inspector
    public int normalHandIndex = 0;
    public int gunHandIndex = 1;
    public int detectiveLensHandIndex = 2;
    public int rayInteractorHandIndex = 3;

    private enum LeftHandState { Normal, DetectiveLens, RayInteractor }
    private enum RightHandState { Normal, Gun, RayInteractor }

    private LeftHandState currentLeftHand = LeftHandState.Normal;
    private RightHandState currentRightHand = RightHandState.Normal;

    void Start()
    {
        ActivateLeftHand(LeftHandState.Normal);
        ActivateRightHand(RightHandState.Normal);
    }

    public void SwitchHands(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0: // Gun
                radialSelection.SetActiveHandTransform(gunHandIndex); // Set active hand transform
                ActivateLeftHand(currentLeftHand == LeftHandState.RayInteractor ? LeftHandState.Normal : currentLeftHand);
                ActivateRightHand(RightHandState.Gun);
                break;

            case 1: // Detective Lens
                radialSelection.SetActiveHandTransform(detectiveLensHandIndex); // Set active hand transform
                ActivateLeftHand(LeftHandState.DetectiveLens);
                ActivateRightHand(currentRightHand == RightHandState.RayInteractor ? RightHandState.Normal : currentRightHand);
                break;

            case 2: // Ray Interactors
                radialSelection.SetActiveHandTransform(rayInteractorHandIndex); // Set active hand transform
                ActivateLeftHand(LeftHandState.RayInteractor);
                ActivateRightHand(RightHandState.RayInteractor);
                break;

            default:
                Debug.LogWarning("Invalid radial part selected: " + selectedIndex);
                break;
        }
    }

    private void ActivateLeftHand(LeftHandState state)
    {
        normalLeftHand.SetActive(state == LeftHandState.Normal);
        detectiveLensLeftHand.SetActive(state == LeftHandState.DetectiveLens);
        rayInteractorLeftHand.SetActive(state == LeftHandState.RayInteractor);

        currentLeftHand = state;
    }

    private void ActivateRightHand(RightHandState state)
    {
        bool isGunActive = state == RightHandState.Gun;
        normalRightHand.SetActive(!isGunActive);
        gunRightHand.SetActive(isGunActive);
        rayInteractorRightHand.SetActive(state == RightHandState.RayInteractor);

        currentRightHand = state;
    }
}

