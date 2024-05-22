using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAnimationTriggers : Enemy_AnimationTriggers
{
    private BossEnemy enemyDeathBringer => GetComponentInParent<BossEnemy>();

    private void Relocate() => enemyDeathBringer.FindPosition();

    //private void MakeInvisible() => enemyDeathBringer.fx.MakeTransparent(true);
    //private void MakeVisible() => enemyDeathBringer.fx.MakeTransparent(false);
}
