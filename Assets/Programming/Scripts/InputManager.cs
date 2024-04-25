using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerController playerControls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;

        playerControls = new PlayerController();
    }

    public Vector2 Movement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public bool Attack()
    {
        return playerControls.Player.Attack.triggered;
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }
}
