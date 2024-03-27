using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    public float _speed;
    public float _jumpForce;
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

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 move = transform.right * xMove + transform.forward * zMove;

        _characterController.Move(move * _speed * Time.deltaTime);

        if(Input.GetButton("Jump") && _characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
       _characterController.Move(velocity * Time.deltaTime);
    }
}
