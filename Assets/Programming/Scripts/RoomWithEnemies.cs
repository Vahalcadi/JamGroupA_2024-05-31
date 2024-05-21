using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWithEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GenericDoor> doors;

    List<Enemy> enemiesScript = new List<Enemy>();

    bool allEnemiesDied;

    bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            if (allEnemiesDied)
                return;

            enemies.ForEach(e => { e.SetActive(true); });
            enemies.ForEach (e => { enemiesScript.Add(e.GetComponent<Enemy>()); });
            doors.ForEach(d => { d.SetIsInteractable(false); d.CloseDoor(); });

            StartCoroutine(CheckDeadEnemies());
        }
    }

    public IEnumerator CheckDeadEnemies()
    {
        while (enabled == true)
        {
            allEnemiesDied = CheckIfAllEnemiesDied();

            Debug.Log(allEnemiesDied);

            if (allEnemiesDied)
            {
                doors.ForEach(d => { d.SetIsInteractable(true); });
                enabled = false;
            }
            yield return null;
        } 
    }

    public bool CheckIfAllEnemiesDied()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.activeSelf == true)
            {
                return false;
            }
        }

        foreach (Enemy enemy in enemiesScript)
        {
            if(!enemy.IsDead)
                return false;
        }

        return true;
    }
}
