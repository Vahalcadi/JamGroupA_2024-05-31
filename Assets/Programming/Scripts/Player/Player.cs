using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 input;
    private InputManager inputManager;
    private bool isAttacking;
    private float attackCooldownTimer;
    private bool isInvincible;
    private bool Move;
    private bool Dash;

    [Header("Player Health")]
    [SerializeField] private int maxHealth; //added for the prototype
    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get; set; }


    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Animator")]
    [SerializeField] private Animator anim;
    [SerializeField] private float attackCooldown;

    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    [Header("Damage")]
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (inputManager.Attack() && !isAttacking && attackCooldownTimer < 0)
        {
            AttackAnimationTrigger();
        }

        input = new Vector3(inputManager.Movement().normalized.x, 0, inputManager.Movement().normalized.y);
        Look();

        #region Damagetest input
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1); //added for the prototype and testing
        }
        #endregion
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
            return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        HUDManager.Instance.UpdateHealthBar();

        if (CurrentHealth == 0)
            GameManager.Instance.EndGame("You Lost!, Play Again?");
    }


    private void Look()
    {
        if (input == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(input.AdjustToIsometricPlane(), Vector3.up);

        /**
         * comment this: transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
         * and uncomment this: transform.rotation = rotation
         * if player has to snap into position instead of rotating into position
         * **/

        /*THIS BELOW*/
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        //transform.rotation = rotation;

    }

    private void Movement()
    {
        rb.MovePosition(transform.position + (transform.forward * input.normalized.magnitude) * speed * Time.deltaTime);
    }

    private void Attack() //animation trigger event
    {
        Collider[] colliders = Physics.OverlapSphere(attackCheck.position, attackCheckRadius);
        GenericDoor animator;
        Torches torch;
        Entity enemy;

        foreach (var hit in colliders)
        {
            Debug.Log(hit.gameObject);

            if ((animator = hit.GetComponent<GenericDoor>()) != null)
            {
                animator.OpenDoor();
            }

            if ((torch = hit.GetComponent<Torches>()) != null)
            {
                torch.AddValueToDoorParent();
            }
            if ((enemy = hit.GetComponent<Entity>()) != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public void AttackAnimationTrigger()
    {


        attackCooldownTimer = attackCooldown;
        ToggleCanAttack();
        anim.SetBool("IsAttacking", isAttacking);
    }

    private void ToggleCanAttack() // used as an animation trigger event
    {
        isAttacking = !isAttacking;
    }

    private void ToggleIsInvincible()
    {
        isInvincible = !isInvincible;
        Dash = !Dash;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
