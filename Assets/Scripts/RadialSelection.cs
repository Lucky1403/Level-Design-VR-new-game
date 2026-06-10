using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.XR;

public class RadialSelection : MonoBehaviour
{
    [Range(2, 10)]
    public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenParts = 10f;
    public List<Transform> handTransforms = new List<Transform>(); // Changed to a list
    public UnityEvent<int> OnPartSelected;
    private List<GameObject> spawnedParts = new List<GameObject>();
    private List<TextMeshProUGUI> spawnedTexts = new List<TextMeshProUGUI>();
    private int CurrentSelectedRadialPart = -1;
    private int activeHandTransformIndex = 0; // Index of the active hand transform

    // Start is called before the first frame update
    void Start()
    {
        SpawnRadialParts();

        SetLabel(0, "Ray Interactors");
        SetLabel(1, "Gun");
        SetLabel(2, "Detective Lens");

        // Ensure there is at least one hand transform
        if (handTransforms.Count == 0)
        {
            Debug.LogError("No hand transforms assigned! Please assign at least one hand transform in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetSelectedRadialPart();
        // Check A button press on Oculus Right Controller
        bool aButtonPressed = false;
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out aButtonPressed) && aButtonPressed)
        {
            // Trigger selection when A button is pressed
            HideAndTriggerSelected();
        }
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(CurrentSelectedRadialPart);
    }

    public void GetSelectedRadialPart()
    {
        // Check if there are any hand transforms
        if (handTransforms.Count == 0)
        {
            Debug.LogWarning("No hand transforms assigned!");
            return;
        }

        // Get the active hand transform
        Transform activeHandTransform = handTransforms[activeHandTransformIndex];

        if (activeHandTransform == null)
        {
            Debug.LogError("Active hand transform is null!  Check that the activeHandTransformIndex is valid and the transform is assigned.");
            return;
        }

        Vector3 centerToHand = activeHandTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);

        if (angle < 0)
        {
            angle += 360;
        }

        // Ensure CurrentSelectedRadialPart is within the valid range
        CurrentSelectedRadialPart = ((int)angle * numberOfRadialPart / 360) % numberOfRadialPart;

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            if (i == CurrentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = Vector3.one * 1.1f;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
    }

    public void SpawnRadialParts()
    {
        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();
        spawnedTexts.Clear();

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = -i * 360 / numberOfRadialPart - angleBetweenParts / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);
            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;

            // spawnedRadialPart.GetComponent<Image>().fillAmount = (1 / (float)numberOfRadialPart) - (angleBetweenParts / 360);

            TextMeshProUGUI text = spawnedRadialPart.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = $"Partt {i + 1}";
                spawnedTexts.Add(text);
            }

            spawnedParts.Add(spawnedRadialPart);
        }

    }

    public void SetLabel(int index, string content)
    {
        if (index >= 0 && index < spawnedTexts.Count)
        {
            spawnedTexts[index].text = content;
        }
    }

    // Method to set the active hand transform
    public void SetActiveHandTransform(int index)
    {
        if (index >= 0 && index < handTransforms.Count)
        {
            activeHandTransformIndex = index;
        }
        else
        {
            Debug.LogError("Invalid hand transform index: " + index);
        }
    }
}
