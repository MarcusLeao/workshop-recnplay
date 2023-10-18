using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;

    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    float movementSpeed = 2f;

    [SerializeField]
    float jumpForce = 5f;
    private float ySpeed;

    [SerializeField]
    bool isGrounded;

    [SerializeField]
    float turnSpeed = 30f;

    //Respawn
    [SerializeField]
    float threshold;

    [SerializeField]
    Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Movement Direction
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction.Normalize();
        float magnitude = direction.magnitude;

        //Jump
        ySpeed += Physics.gravity.y * Time.deltaTime;
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

        //Move and Rotate
        if (direction != Vector3.zero)
        {
            //Rotate
            Quaternion toRotate = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotate,
                turnSpeed * Time.deltaTime
            );

            //Move
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
            GameController.health -= 1;
        }
    }
}
