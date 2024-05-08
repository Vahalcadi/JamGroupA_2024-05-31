using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 input;
    private InputManager inputManager;
    private bool isAttacking;

    [Header("Player Health")]
    [SerializeField] private int maxHealth; //added for the prototype
    public int MaxHealth { get { return  maxHealth; } }
    public int CurrentHealth { get; set; }


    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //vector = inputManager.Movement().normalized;

        if (inputManager.Attack() && !isAttacking)
        {
            AttackAnimationTest();
            Attack();
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

    private void AttackAnimationTest()
    {
        anim.SetTrigger("isAttacking");
    }

    private void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(attackCheck.position, attackCheckRadius);
        GenericDoor animator;

        foreach (var hit in colliders)
        {
            Debug.Log(hit.gameObject);

            if ((animator = hit.GetComponent<GenericDoor>()) != null)
            {
                animator.OpenDoor();
            }
                
        }
    }

    private void ToggleCanAttack() // used as an animation trigger event
    {
        isAttacking = !isAttacking;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
