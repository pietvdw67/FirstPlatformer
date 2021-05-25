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
    private bool firePressed;
    private bool isGrounded;
    private bool isDelayGroundCheck;
    public float radius = 0.3f;

    float idleCounter = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        sideMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
        idleCounter++;

        if (jumpPressed == false)
        {
            jumpPressed = Input.GetKeyDown(KeyCode.Space);            
        }

        if (firePressed == false)
        {
            firePressed = Input.GetKeyDown(KeyCode.LeftControl);
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();
        SetGrounded();
        PlayerJump();
        PlayerIdle();
        PlayerFire();

    }

    private void PlayerFire()
    {
        if (firePressed)
        {
            playerAnim.PlayFire(true);
            firePressed = false;
            Invoke("StopFire", 0.25f);

        }
    }

    private void StopFire()
    {
        playerAnim.PlayFire(false);
    }

    private void PlayerIdle()
    {
        // Wave currently only when looking right
        if (idleCounter >= 1000 && transform.localScale.x == 1)
        {
            playerAnim.PlayIdleWave(true);
        }
    }

    private void PlayerMove()
    {
        body.velocity = new Vector3(sideMovement, body.velocity.y, 0);

        if (sideMovement > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            playerAnim.PlayRunAnimation(true);
            playerAnim.PlayIdleWave(false);
            idleCounter = 0;

        }
        else if (sideMovement < 0)
        {
            playerAnim.PlayRunAnimation(true);
            transform.localScale = new Vector3(-1f, 1, -1);
            playerAnim.PlayIdleWave(false);
            idleCounter = 0;
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

    private void StopIdleWave()
    {
        idleCounter = 0;
        playerAnim.PlayIdleWave(false);
        
    }

    

}
