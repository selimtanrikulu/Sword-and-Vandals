using System;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerStateController _stateController;

    public float MovementSpeed { get; set; }

    public float yVelocity { get; set; }

    public float horizontal { get; set; }
    public float vertical { get; set; }


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _stateController = GetComponent<PlayerStateController>();
        MovementSpeed = MovementConfig.runningMovementSpeed;
    }

    void Update()
    {
        Move();
        HandleRotation();
    }

    private void HandleRotation()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
        {
            transform.Rotate(Vector3.up, MovementConfig.rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        }
    }

    public void GetStunned()
    {
        if (_stateController.movementState is MovementState.RollBackward or MovementState.RollForward or MovementState.RollRight or MovementState.RollLeft) return;
        
        _stateController.movementState = MovementState.Stunned;
        _stateController.attackState = AttackState.None;
    }

    private void Move()
    {
        Vector3 moveDist = new Vector3();

        switch (_stateController.movementState)
        {
            case MovementState.Move:
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
                Transform myTransform = transform;
                Vector3 verticalMove = myTransform.forward *
                                       (vertical * MovementConfig.runningMovementSpeed * Time.deltaTime);
                Vector3 horizontalMove = myTransform.right *
                                         (horizontal * MovementConfig.runningMovementSpeed * Time.deltaTime);
                moveDist = verticalMove + horizontalMove;
                break;

            case MovementState.RollForward:
                moveDist = transform.forward * (MovementSpeed * Time.deltaTime);
                break;

            case MovementState.RollBackward:
                moveDist = -transform.forward * (MovementSpeed * Time.deltaTime);
                break;

            case MovementState.RollRight:
                moveDist = transform.right * (MovementSpeed * Time.deltaTime);
                break;

            case MovementState.RollLeft:
                moveDist = -transform.right * (MovementSpeed * Time.deltaTime);
                break;


            case MovementState.Stunned:
                //nothing
                break;
        }

        //Apply gravity
        if (!_characterController.isGrounded)
        {
            yVelocity += MovementConfig.gravity * Time.deltaTime;
        }


        moveDist += Vector3.up * (Time.deltaTime * yVelocity);

        _characterController.Move(moveDist);
    }
}