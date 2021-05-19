using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerAnimation playerAnim;

    public float moveSpeed = 5f;

    private Rigidbody body;
    private Animation anim;

    float sideMovement;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        sideMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }
    private void FixedUpdate()
    {
        playerMove();
    }

    private void playerMove()
    {
        body.velocity = new Vector3(sideMovement, 0, 0);

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

}
