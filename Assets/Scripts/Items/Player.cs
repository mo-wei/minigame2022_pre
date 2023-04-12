using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //可操纵物体的基类


    public Rigidbody2D m_rigidbody;
    protected CapsuleCollider2D m_CapsulleCollider;
    //运动参数
    public float moveSpeed = 1;
    public float jumpSpeed = 1;
    public int jumpTimes = 0;//最大跳跃次数
    public int jumpTime;//现跳跃次数
    public bool isGround = true;

    public bool isControl = false;

    protected Collider2D InteractObject;
    private void Awake()
    {
        jumpTime = jumpTimes;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    //接受输入命令时物体的具体实现
    public virtual void Move(Vector2 data)
    {

    }
    public virtual void Jump()
    {

    }
    public virtual void Gliding()
    {

    }
    public virtual void Broke()
    {

    }
    public virtual void Interaction()
    {

    }
    public virtual void ChangeControl()
    {
        isControl = !isControl;
    }
    //控制是否显示扫光特效
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TimeWindow" && collision.transform != transform.Find("TimeWindow"))
        {
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect",1);//打开扫光特效
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        InteractObject = collision;
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TimeWindow" && collision.transform != transform.Find("TimeWindow"))
        {
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect", 0);//关闭扫光特效
        }
        InteractObject = null;
    }

}
