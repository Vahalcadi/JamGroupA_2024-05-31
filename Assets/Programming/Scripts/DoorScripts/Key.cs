using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickable
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);

            GameManager.Instance.CollectKey();

            gameObject.SetActive(false);
        }
    }
}
