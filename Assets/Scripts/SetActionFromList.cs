using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActionFromList : MonoBehaviour
{
    public List<GameObject> actionsList;

    void Start()
    {
        foreach (var action in actionsList)
        {
            action.SetActive(false);
        }
    }

    public void SetAction(int index)
    {
        if (index >= 0 && index < actionsList.Count)
        {
            for (int i = 0; i < actionsList.Count; i++)
            {
                actionsList[i].SetActive(i == index);
            }
        }
        else
        {
            Debug.LogWarning("Index out of range: " + index);
        }
    }
}

// XR Origin mai ab iss script ke inspector mein actionsList mai objects daalne hai 
