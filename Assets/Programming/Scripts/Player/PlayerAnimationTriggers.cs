using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAnimationTriggers : MonoBehaviour
{
    [SerializeField] private ParticleSystem weaponTrails;
    private Player player => GetComponentInParent<Player>();

    private void PlayVFX()
    {
        weaponTrails.Play();
    }

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger() //animation trigger event
    {
        Collider[] colliders = Physics.OverlapSphere(player.attackCheck.position, player.attackCheckRadius);
        GenericDoor animator;
        Torches torch;
        Entity enemy;

        foreach (var hit in colliders)
        {
            Debug.Log(hit);
            if (hit.CompareTag("Bullet"))
                Destroy(hit.gameObject);

            if ((animator = hit.GetComponent<GenericDoor>()) != null)
            {
                animator.OpenDoor();
            }
            if ((torch = hit.GetComponent<Torches>()) != null)
            {
                torch.AddValueToDoorParent();
            }
            if ((enemy = hit.GetComponent<Enemy>()) != null)
            {
                enemy.TakeDamage(player.Damage);
            }
        }
    }
}
