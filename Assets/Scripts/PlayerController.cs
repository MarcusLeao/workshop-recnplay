using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;

    [SerializeField]
    float movementSpeed = 2f;

    [SerializeField]
    float jumpForce = 5f;
    float ySpeed;

    [SerializeField]
    float turnSpeed = 30f;
    public bool isGrounded;
    public CharacterController characterController;

    public float threshold;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction.Normalize();
        float magnitude = direction.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            ySpeed = -0.5f;
            isGrounded = false;
        }

        Vector3 vel = direction * magnitude;
        vel.y = ySpeed;
        characterController.Move(vel * Time.deltaTime);

        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;
            isGrounded = true;
            _animator.SetBool("Jump", false);
            if (Input.GetButtonDown("Jump"))
            {
                _animator.SetBool("Jump", true);
                ySpeed = jumpForce;
                isGrounded = false;
            }
        }

        if (direction != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotate,
                turnSpeed * Time.deltaTime
            );

            _animator.SetBool("Move", true);
            characterController.SimpleMove(direction * magnitude * movementSpeed);
        }
        else
        {
            _animator.SetBool("Move", false);
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = spawnPoint.position;
        }
    }
}
