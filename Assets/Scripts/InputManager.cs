using EC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerControls;

    private GameManager gameManager;

    private void Awake()
    {
        playerControls = new PlayerControls();
        gameManager = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Movement.Move.performed += ctx => gameManager.PieceMovement();
    }

    private void Update()
    {
        
    }
}
