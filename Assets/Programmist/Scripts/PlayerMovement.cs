using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    public float _speed;
    public float _runSpeed;
    public float _jumpForce;
    private bool isRunning = false;
    private CharacterController _characterController;
    private Vector3 velocity;

    [Header("Camera")]
    public float sensitivity;
    public GameObject playerCamera;
    private float rotationX = 0f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Проверяем, что стоим на земле
    private bool IsGrounded()
    {
        RaycastHit hit;
        float distanceToGround = 0.1f;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distanceToGround))
            return true;
        else
            return false;
    }

    void Update()
    {
         //Камера
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        //Включение/отключение бега
        if((Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded()))
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        //Проверка на бег
        float moveSpeed = isRunning ? _runSpeed : _speed;
        
        //Движение
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 move = transform.right * xMove + transform.forward * zMove;

        _characterController.Move(move * moveSpeed * Time.deltaTime);

        //Прыжок
        if (Input.GetButton("Jump") && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        _characterController.Move(velocity * Time.deltaTime);
    }
}
