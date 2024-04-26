using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isOpen;
    private bool isInteractable;

    private void Start()
    {
        isInteractable = true;
    }

    public void OpenDoor()
    {
        if (!isInteractable)
            return;

        isOpen = !isOpen;
        anim.SetBool("isOpen",isOpen);
    }

    public void ToggleIsInteractable()
    {
        isInteractable = !isInteractable;
    }
}
