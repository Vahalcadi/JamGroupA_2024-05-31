using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isOpen;
    private bool isInteractable;

    public virtual void Start()
    {
        isInteractable = true;
    }

    public virtual void OpenDoor()
    {
        if (!isInteractable)
            return;

        isOpen = !isOpen;
        anim.SetBool("isOpen",isOpen);
    }

    public virtual void ToggleIsInteractable()
    {
        isInteractable = !isInteractable;
    }
}
