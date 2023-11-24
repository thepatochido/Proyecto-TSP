using UnityEngine;

public class FSPControllerScript : MonoBehaviour
{
    public bool jump;
    public bool isGrounded;
    public bool isRunning; //2
    public float x, z;
    public float speed;
    public float runSpeed; //2
    public float mouseSensitivity;
    public float smoothRotation = 0.004f; //3
    public float gravity = 9.81f;
    public float jumpSpeed;
    public Camera camera1;
    public CharacterController character;
    //public Animator animator; //4
    
    public Vector3 velocity;
    private float rightLeftRotation;
    private float upDownRotation;
    private float rotationSpeedX = 0.0f; //3
    private float rotationSpeedY = 0.0f; //3

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;         
    }

    void Update()
    {
        ///////////////////////// Rotation Inputs //////////////////////////
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
        rightLeftRotation += mouseX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;
        upDownRotation -= mouseY;
        upDownRotation = Mathf.Clamp(upDownRotation, -70, 70);
        ////////////////////////////////////////////////////////////////////

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");
        isGrounded = character.isGrounded;
        isRunning = Input.GetKey(KeyCode.LeftShift); //2
    }

    private void FixedUpdate()
    {
        RotateLeftRight();
        RotateUpDown();
        Move();
    }

    private void RotateLeftRight()
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rightLeftRotation, ref rotationSpeedY, smoothRotation); //3
        transform.rotation = Quaternion.Euler(0, angle, 0); //2
    }

    private void RotateUpDown()
    {
        float angle = Mathf.SmoothDampAngle(camera1.transform.eulerAngles.x, upDownRotation, ref rotationSpeedX, smoothRotation); //3
        camera1.transform.localRotation = Quaternion.Euler(angle, 0, 0); //2
    }

    private void Move()
    {
        Vector3 moveVector = transform.right * x + transform.forward * z;

        if (moveVector == Vector3.zero) //Si no me muevo
        {
            //animator.SetFloat("Speed", 0f); //Ejecuta Idle
        }
        
        else if(!isRunning && moveVector != Vector3.zero) //No debe de correr, camina
        {
            character.Move(moveVector.normalized * speed * Time.deltaTime * 10);
            //animator.SetFloat("Speed", 0.5f); //Ejecuta Idle
        }

        else if(isRunning && moveVector != Vector3.zero)  //Debe de correr
        {
            character.Move(moveVector.normalized * runSpeed * Time.deltaTime * 10);
           //animator.SetFloat("Speed", 1); //Ejecuta Idle
        }

        if (jump && isGrounded)
        {
            velocity.y = jumpSpeed / 20;
        }

        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime * Time.deltaTime; 
        }

        character.Move(velocity);
    }
}
