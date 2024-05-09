using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DoorWithTorches : GenericDoor
{
    [Header("Numerical combination")]
    [SerializeField] private List<int> combination;
    [Header("Torches")]
    [SerializeField] private List<Torches> torches;
    private List<int> playerCombination = new List<int>();
    private bool checkAddedCorrectNumber;
    public override void Start()
    {
        isInteractable = false;
    }

    public override void OpenDoor()
    {
        return;
    }

    public override void ToggleIsInteractable()
    {
        base.ToggleIsInteractable();
    }

    public void AddValueToPlayerCombination(int value)
    {
        playerCombination.Add(value);
        CheckCombination();
    }

    private void CheckCombination()
    {
        for(int i = 0; i < playerCombination.Count; i++)
        {
            if (playerCombination[i] == combination[i])
                checkAddedCorrectNumber = true;
            else
            {
                checkAddedCorrectNumber = false;
                break;
            }
        }

        if (!checkAddedCorrectNumber)
        {
            torches.ForEach(torch => torch.ResetTorch());
            playerCombination.Clear();
        }
        else
            RunCombination();
    }

    private void RunCombination()
    {
        if (playerCombination.Count == combination.Count)
        {
            isOpen = true;
            anim.SetBool("isOpen", isOpen);
        }

    }
}
