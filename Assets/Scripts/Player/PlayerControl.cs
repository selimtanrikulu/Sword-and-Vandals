using System;
using UnityEngine;



public enum AttackState
{
    None = 0,
    Attack1 = 1,
    Attack2 = 2,



}


public class PlayerControl : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float movementSpeed;

    private Animator _animator;

    private float horizontal;
    private float vertical;


    [SerializeField] private float rotationSpeed;

    AttackState attackState;
    

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontal = Input.GetAxis("Horizontal");
        vertical =Input.GetAxis("Vertical");


        var myTransform = transform;
        Vector3 verticalMove = myTransform.forward * (vertical * Time.deltaTime * movementSpeed);
        Vector3 horizontalMove = myTransform.right * (horizontal * Time.deltaTime * movementSpeed);
        _characterController.Move(verticalMove + horizontalMove);
        
        
        _animator.SetFloat("x",horizontal);
        _animator.SetFloat("y",vertical);


        


        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
        {
            transform.Rotate(Vector3.up,rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        }
        
        HandleAttackAnimation();
        
    }

    private void HandleAttackAnimation()
    {
        if(attackState == AttackState.Attack2)
        {
            if(Input.GetKeyUp(KeyCode.Mouse1))
            {
                ResetAttackState();
            }
        }


        if(attackState != AttackState.None) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackState = AttackState.Attack1;
        }

        else if(Input.GetKeyDown(KeyCode.Mouse1))
        { 
            attackState = AttackState.Attack2;
        }


        

        _animator.SetInteger("AttackState",(int)attackState);
    }

    public void AttackOccurred()
    {
        //TODO
        
    }


    public void ResetAttackState()
    {
        attackState = AttackState.None;
    }
}
