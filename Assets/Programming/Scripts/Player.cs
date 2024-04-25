using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 vector;
    private Vector3 input;
    private InputManager inputManager;

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //vector = inputManager.Movement().normalized;

        if (inputManager.Attack())
            AttackAnimationTest();

        input = new Vector3(inputManager.Movement().normalized.x, 0, inputManager.Movement().normalized.y);
        Look();
    }

    private void FixedUpdate()
    {
        Movement();
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
}
