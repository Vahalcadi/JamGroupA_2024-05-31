
using UnityEngine;

public class BossEnemy : Enemy
{
    public BoxCollider cd;
    public LayerMask whatIsGround;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider arena;
    [SerializeField] private Vector3 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;
    public bool bossFightBegun;
    public float idleTime = 2;
    public float battleTime = 7;

    public BossEnemyIdleState IdleState { get; private set; }
    public BossEnemyBattleState BattleState { get; private set; }
    public BossEnemyAttackState AttackState { get; private set; }
    //public DeathBringerDeadState deadState { get; private set; }
    //public DeathBringerSpellCastState spellCastState { get; private set; }
    public BossEnemyTeleportState TeleportState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new BossEnemyIdleState(this, stateMachine, "Idle", this);
        BattleState = new BossEnemyBattleState(this, stateMachine, "Move", this);
        AttackState = new BossEnemyAttackState(this, stateMachine, "Attack", this);
        //spellCastState = new DeathBringerSpellCastState(this, stateMachine, "Cast", this);
        TeleportState = new BossEnemyTeleportState(this, stateMachine, "Teleport", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }


    public override void Die()
    {
        base.Die();
        //stateMachine.ChangeState(deadState);

    }


    /*public void CastSpell()
    {
        Player player = PlayerManager.Instance.Player;

        float xOffset = 0;

        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * 3;


        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + 1.5f);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);

    }*/

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.z + 3, arena.bounds.max.z - 3);

        transform.position = new Vector3(x, transform.position.y, y);

        if (!GroundBelow() || SomethingIsAround())
        {
            Debug.Log("looking for new position");
            FindPosition();
        }
    }

    private bool GroundBelow() => Physics.Raycast(transform.position, transform.rotation * Vector3.down, 100, whatIsGround);
    //private bool SomethingIsAround() => Physics.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);
    private bool SomethingIsAround() => Physics.BoxCast(transform.position, surroundingCheckSize, Vector3.zero, Quaternion.identity, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }


        return false;
    }

    /*public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {

            return true;
        }

        return false;
    }*/

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
    }
}
