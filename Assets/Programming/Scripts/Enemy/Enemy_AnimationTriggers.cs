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

    private void AttackTrigger()
    {
        enemy.AnimationAttackTrigger();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        Player player;

        foreach (var hit in colliders)
        {
            if ((player = hit.GetComponent<Player>()) != null)
            {
                player.TakeDamage(enemy.Damage);
            }
        }


    }
}
