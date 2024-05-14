using System.Collections.Generic;
using UnityEngine;

public class RoomWithEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GenericDoor> doors;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemies.ForEach(e => { e.SetActive(true); });
            doors.ForEach(d => { d.SetIsInteractable(false); d.CloseDoor(); });
        }
    }
}
