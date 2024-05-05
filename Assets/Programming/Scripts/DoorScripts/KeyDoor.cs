using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KeyDoor : GenericDoor
{
    private Animator animKey;
    private bool isKeyOpen;
    private bool isKeyInteractable;
    private bool isAlreadyOpen = false;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    public override void OpenDoor()
    {
        if (!isKeyInteractable)
            return;
        else if (gameManager.numberOfKeysCollected > 0 && !isAlreadyOpen)
        {
            gameManager.numberOfKeysCollected--;
            isKeyOpen = !isKeyOpen;
            isAlreadyOpen = true;
            animKey.SetBool("isOpen", isKeyOpen);
            return;
        }
        else if (isAlreadyOpen == true)
        {
            isKeyOpen = !isKeyOpen;
            animKey.SetBool("isOpen", isKeyOpen);
            return;
        }
    }
}
