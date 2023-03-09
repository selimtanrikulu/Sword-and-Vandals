using UnityEngine;
using Zenject;


public class PlayerControl : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float movementSpeed;

    private Animator _animator;


    private float horizontal;
    private float vertical;


    [SerializeField] private float rotationSpeed;
    private ITestManager _testManager;


    [Inject]
    void Inject(ITestManager testManager)
    {
        _testManager = testManager;
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        
        Debug.Log(_testManager.GetValue());
        _testManager.SetValue(10);
        Debug.Log(_testManager.GetValue());
        _testManager.IncreaseValue(3);
        Debug.Log(_testManager.GetValue());
        _testManager.IncrementValue();
        Debug.Log(_testManager.GetValue());
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


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.SetTrigger("Attack1Trigger");
        }


        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
        {
            transform.Rotate(Vector3.up,rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        }
        
        
    }



    public void AttacOccurred()
    {
        
    }
}
