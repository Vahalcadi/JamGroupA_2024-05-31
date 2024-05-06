public class KeyDoor : GenericDoor
{
    /*private Animator animKey;
    private bool isKeyOpen;
    private bool isKeyInteractable;
    private bool isAlreadyOpen = false;*/

    public override void Start()
    {
        isInteractable = false;
    }

    public override void OpenDoor()
    {
        if (isOpen)
            return;

        if (GameManager.Instance.numberOfKeysCollected > 0)
        {
            isInteractable = true;
            GameManager.Instance.numberOfKeysCollected--;
        }
        else
            isInteractable = false;

        base.OpenDoor();
    }

    /*public override void OpenDoor()
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
    }*/
}
