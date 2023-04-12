using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Player
{
    public float glidVerticalV = 0.1f;
    private Animator animator;
    private float lastX;
    public GameObject panel;
    public GameObject ControlObject;
    public Transform TransportPosition;
    public GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Move(Vector2 data)
    {
        if(!GameManager.GetComponent<GameManager>().isFuture)
        { if (data.x != 0)
            {
                lastX = data.x;
            }
            m_rigidbody.velocity = new Vector2(data.x * moveSpeed, m_rigidbody.velocity.y);
            animator.SetFloat("RunNow", lastX); 
        }
    }
    public override void Jump()
    {
        if (!GameManager.GetComponent<GameManager>().isFuture)
        {
            isGround = false;
            if (jumpTime > 0)
            {
                animator.SetBool("IsFly", true);
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpSpeed);
                jumpTime -= 1;
            }
        }
    }
    public override void Gliding()
    {
        if(!isGround && !GameManager.GetComponent<GameManager>().isFuture)
        {
            if(-glidVerticalV >= m_rigidbody.velocity.y)
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, -glidVerticalV);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interaction()
    {
        if(InteractObject && InteractObject.tag == "Mail") 
        {
            panel.GetComponent<TextType>().ShowText(1);
            StartCoroutine("ChangePosition");
        }
    }
    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(1f);

        BGM.instance.BGM2();
        ControlObject.GetComponent<ControlObject>().Item.transform.position = TransportPosition.position;
        ControlObject.GetComponent<ControlObject>().Yoffset = 7.6F;
        GameManager.GetComponent<GameManager>().IsIn2 = true;
        GameManager.GetComponent<GameManager>().ViewChange();
    }
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TimeWindow" && collision.transform != transform.Find("TimeWindow"))
        {
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect", 1);//打开扫光特效
        }
    }
    protected new void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mail")
        {
            InteractObject = collision;
        }
    }
    protected new void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TimeWindow" && collision.transform != transform.Find("TimeWindow"))
        {
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect", 0);//关闭扫光特效
        }
        InteractObject = null;
    }
}
