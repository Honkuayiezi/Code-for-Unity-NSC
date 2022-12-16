using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private float rotation_speed =5;
    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravity;
    
    private void Awake() {
        RegisterCharacter();
    }

    private void Start() {
        gravity = Game_Manager.gravity;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    private void Update() {
        Move();
    }

    private void Move() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0;
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x,0,input.y);

        move = move.x * camTransform.right.normalized + move.z * camTransform.forward.normalized;
        move.y = 0;
        
        controller.Move(move * Time.deltaTime * speed);
        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
            Run();
        }
        else {
            Idle();
        }

        float targetAngle = camTransform.eulerAngles.y;
        Quaternion targetRotataion = Quaternion.Euler(0,targetAngle,0);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotataion,rotation_speed*Time.deltaTime);



    }

    private void Idle() {
        anim.SetFloat("normal_speed",0,0.1f,Time.deltaTime);
    }
    private void Run() {
        anim.SetFloat("normal_speed",1,0.1f,Time.deltaTime);

    }
}
