using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speedRotation;
    private Vector3 moveDirection;
    private Vector2 moveReadings;
    private Rigidbody rb;
    public Transform target;
    InputSystem_Actions userInput;

    private void Awake()
    {
        userInput = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        userInput.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        moveReadings = userInput.Player.Move.ReadValue<Vector2>();
        moveDirection.x = moveReadings.x;
        moveDirection.z = moveReadings.y;
        moveDirection = moveDirection.normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed *  Time.fixedDeltaTime;
    }

    private void OnMove()
    {
        Debug.Log("MOVE");
    }
}
