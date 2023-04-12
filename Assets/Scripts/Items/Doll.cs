using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll: Player
{
    private Animator animator;
    private float lastX;

    public Sprite Sit;
    public Sprite Stand;
    //ºÃ≥–Player¿‡
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Move(Vector2 data)
    {
        if(data.x != 0)
        {
            animator.SetBool("IsRunning", true);
            lastX = data.x;
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        animator.SetFloat("RunNow", lastX);

        m_rigidbody.velocity = new Vector2(data.x*moveSpeed, m_rigidbody.velocity.y);
    }
    public override void Jump()
    {
        isGround = false;
        if (jumpTime > 0)
        {
            animator.SetBool("IsJump", true);
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpSpeed);
            jumpTime -= 1;        
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ChangeControl()
    {
        isControl = !isControl;
        if(isControl)
        {
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0);
            animator.SetBool("IsControl", true);
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0,0.42f);
            animator.SetBool("IsControl", false);
        }
    }
}
