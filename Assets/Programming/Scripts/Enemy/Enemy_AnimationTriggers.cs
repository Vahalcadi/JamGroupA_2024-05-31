using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void PlayAttackVFX()
    {
        enemy.attackVFX.Play();
    }

    private void AttackTrigger()
    {
        enemy.AnimationAttackTrigger();

        Collider[] colliders = Physics.OverlapSphere(enemy.attackCheck.position, enemy.attackCheckRadius);

        Player player;

        foreach (var hit in colliders)
        {
            
            Debug.Log(hit);

            if ((player = hit.GetComponent<Player>()) != null)
            {
                if (player.IsInvincible)
                    return;

                Debug.Log("Damage Player");
                Debug.Log(enemy.Damage);
                player.TakeDamage(enemy.Damage);
            }
            
        }
    }
}
