using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    public Vector3 Input { get; set; }
    public InputManager InputManager { get; set; }
    private float attackCooldownTimer;

    [Header("Animator")]
    [SerializeField] private float attackCooldown;

    [Header("Movement")]
    [SerializeField] private float turnSpeed;
    public float dashSpeed;
    public float dashDuration;

    /*[Header("Damage")]
    [SerializeField] private int damage;*/


    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, stateMachine, "Idle");
        MoveState = new PlayerMoveState(this, stateMachine, "Move");
        DashState = new PlayerDashState(this, stateMachine, "Dash");
        AttackState = new PlayerAttackState(this, stateMachine, "Attack");

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        InputManager = InputManager.Instance;

        stateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        if (Time.timeScale == 0)
            return;

        base.Update();

        stateMachine.CurrentState.Update();
        CheckForDashInput();
        
    }

    private void FixedUpdate()
    {
        //Movement();
    }

    public void AnimationTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {

        if (InputManager.Dash())
        {
            stateMachine.ChangeState(DashState);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        HUDManager.Instance.UpdateHealthBar();
        if (CurrentHP == 0)
            GameManager.Instance.EndGame("You Lost!, Play Again?");
    }

    public void Look()
    {
        if (Input == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(Input.AdjustToIsometricPlane(), Vector3.up);

        /**
         * comment this: transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
         * and uncomment this: transform.rotation = rotation
         * if player has to snap into position instead of rotating into position
         * **/

        /*THIS BELOW*/
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        //transform.rotation = rotation;

    }

    public void Movement()
    {
        rb.MovePosition(transform.position + (transform.forward * Input.normalized.magnitude) * Speed * Time.fixedDeltaTime);
        //rb.velocity = transform.position + (transform.forward * Input.normalized.magnitude) * Speed * Time.deltaTime;
    }
}
