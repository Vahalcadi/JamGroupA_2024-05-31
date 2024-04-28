using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyFlames : Pickable
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);

            GameManager.Instance.CollectFlame();

            gameObject.SetActive(false);
        }
    }
}
