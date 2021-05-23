using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    public void PlayRunAnimation(bool isRun)
    {
        anim.SetBool("isRun", isRun);
    }

    public void PlayJumpAnimation(bool isJump)
    {
        anim.SetBool("isJump", isJump);
    }
}
