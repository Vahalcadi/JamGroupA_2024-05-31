using UnityEngine;

public class GenericDoor : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    protected bool isOpen;
    protected bool isInteractable;

    public virtual void Start()
    {
        isInteractable = true;
    }

    public virtual void OpenDoor()
    {
        if (!isInteractable)
            return;

        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
    }

    public void CloseDoor()
    {
        isOpen = false;
        anim.SetBool("isOpen", isOpen);
    }

    public virtual void ToggleIsInteractable()
    {
        isInteractable = !isInteractable;
    }

    public void SetIsInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }
}
