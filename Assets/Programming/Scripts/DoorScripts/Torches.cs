using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torches : MonoBehaviour
{
    [SerializeField] private int combinationValue;
    private bool isInteractable = true;

    DoorWithTorches DoorWithTorches;

    private void Start()
    {
        DoorWithTorches = gameObject.transform.parent.GetComponentInChildren<DoorWithTorches>();
    }

    public void AddValueToDoorParent()
    {
        if (isInteractable)
        {      
            isInteractable = false;
            Debug.LogWarning(combinationValue);

            DoorWithTorches.AddValueToPlayerCombination(combinationValue);
        }
    }

    public void ResetTorch()
    {
        isInteractable = true;
    }
}
