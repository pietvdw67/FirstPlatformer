using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform groundCheckPosition;
    public LayerMask layerObstacles;

    PlayerAnimation playerAnim;

    private float moveSpeed = 5f;
    private float jumpForce = 300f;

    private Rigidbody body;
    private Animation anim;

    float sideMovement;
    private bool jumpPressed;
    private bool isGrounded;
    private bool isDelayGroundCheck;
    public float radius = 0.3f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        sideMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (jumpPressed == false)
        {
            jumpPressed = Input.GetKeyDown(KeyCode.Space);            
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();
        SetGrounded();
        PlayerJump();

    }

    private void PlayerMove()
    {
        body.velocity = new Vector3(sideMovement, body.velocity.y, 0);

        if (sideMovement > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            playerAnim.PlayRunAnimation(true);

        }
        else if (sideMovement < 0)
        {
            playerAnim.PlayRunAnimation(true);
            transform.localScale = new Vector3(-1f, 1, -1);
        }
        else
        {
            playerAnim.PlayRunAnimation(false);
        }
    }

    private void PlayerJump()
    {
        if (!isGrounded)
        {
            return;
        }

        if (jumpPressed)
        {            
            
            playerAnim.PlayJumpAnimation(true);

            // Added delay for visual jumping timing
            Invoke("JumpWithDelay", 0.25f);
            jumpPressed = false;
            isGrounded = false;
            isDelayGroundCheck = true;
            Invoke("stopDelayGroundCheck", 0.5f);

        }
    }

    private void JumpWithDelay()
    {
        body.AddForce(jumpForce * Vector3.up);
    }

    private void SetGrounded()
    {
        if (!isDelayGroundCheck)
        {   
            isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerObstacles).Length > 0;            
            playerAnim.PlayJumpAnimation(!isGrounded);
        }
    }

    private void stopDelayGroundCheck()
    {
        isDelayGroundCheck = false;
    }

}
