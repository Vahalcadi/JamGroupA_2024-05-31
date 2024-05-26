using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius = 1.2f;

    [Header("Stats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    private int currentHP;

    [Header("Knockback info")]
    [SerializeField] protected float knockbackPower;
    [SerializeField] protected float knockbackDuration = .07f;
    protected bool isKnocked;

    public int Damage { get { return damage; } }
    public int CurrentHP { get { return currentHP; } }
    public int MaxHealth { get { return maxHealth; } }

    public int Speed { get { return speed; } }

    public bool IsDead { get; private set; }
    public bool IsInvincible { get; private set; }

    protected virtual void Awake()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        currentHP = maxHealth;

        IsDead = false;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHealth);

        StartCoroutine(HitKnockback());

        if (currentHP == 0)
            Die();
    }

    public IEnumerator HitKnockback()
    {
        isKnocked = true;

        //rb.velocity = -transform.forward * knockbackPower;

        rb.AddForce(-transform.forward * knockbackPower, ForceMode.VelocityChange);

        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }

    public void MakeInvincible(bool _invincible) => IsInvincible = _invincible;

    public virtual void SetZeroVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public virtual void MoveToPlayer()
    {
        //Debug.Log("Moving to player");
        //rb.velocity = new Vector2(_xVelocity, _yVelocity);
        var direction = (PlayerManager.Instance.Player.transform.position - transform.position).normalized;

        var rotateAmount = Vector3.Cross(direction.normalized, transform.forward);

        rotateAmount = new Vector3(0, rotateAmount.y, 0);

        rb.angularVelocity = -rotateAmount * 10;

        rb.velocity = transform.forward * speed;
    }


    public virtual void Die()
    {
        IsDead = true;
        gameObject.SetActive(false);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
