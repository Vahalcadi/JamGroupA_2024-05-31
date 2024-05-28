using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyFlames : Pickable
{
    [Header("The number of the flame in the UI. It should range from 1 to X")]
    [SerializeField] private int flameNumber;
    [SerializeField] private GenericDoor eventDoor;

    private void Start()
    {
        eventDoor.SetIsInteractable(false);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);

            GameManager.Instance.CollectFlame();
            HUDManager.Instance.UpdateFlameSpriteUI(flameNumber);
            gameObject.SetActive(false);
            eventDoor.SetIsInteractable(true);
            eventDoor.OpenDoor();
        }
    }
}


