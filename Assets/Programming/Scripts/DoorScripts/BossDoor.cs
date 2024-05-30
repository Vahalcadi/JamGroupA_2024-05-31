using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : GenericDoor
{
    public override void Start()
    {
        isInteractable = false;
    }

    public override void OpenDoor()
    {
        if (isOpen)
            return;

        if (GameManager.Instance.NumberOfFlamesCollected >= GameManager.Instance.TotalNumberOfFlames)
        {
            isInteractable = true;
        }
        else
            isInteractable = false;

        base.OpenDoor();
    }
}
