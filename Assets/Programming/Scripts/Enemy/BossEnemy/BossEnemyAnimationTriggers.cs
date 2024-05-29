using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAnimationTriggers : Enemy_AnimationTriggers
{
    private BossEnemy BossEnemy => GetComponentInParent<BossEnemy>();

    private void Relocate() => BossEnemy.FindPosition();

    private void Fire() => BossEnemy.FireProjectiles();
    //private void MakeInvisible() => enemyDeathBringer.fx.MakeTransparent(true);
    //private void MakeVisible() => enemyDeathBringer.fx.MakeTransparent(false);
}
