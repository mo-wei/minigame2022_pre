using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wife : Player
{
    // Start is called before the first frame update
    private Animator animator;

    private float lastX;
    public GameObject tip;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Move(Vector2 data)
    {
        if (data.x != 0)
        {
            animator.SetBool("IsWalk", true);
            lastX = data.x;
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
        animator.SetFloat("RunNow", lastX);

        m_rigidbody.velocity = new Vector2(data.x * moveSpeed, m_rigidbody.velocity.y);
    }
    public override void Jump()
    {
        isGround = false;
        if (jumpTime > 0)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpSpeed);
            jumpTime -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interaction()
    {
        if(InteractObject != null && InteractObject.transform.tag == "LaserGun")
        {
            AudioManager.instance.LightAudio();
            InteractObject.gameObject.GetComponent<LaserShoot>().ShootLaser();
            tip.SetActive(false);
        }
    }
}
