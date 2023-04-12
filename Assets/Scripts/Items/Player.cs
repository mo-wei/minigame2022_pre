using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�ɲ�������Ļ���


    public Rigidbody2D m_rigidbody;
    protected CapsuleCollider2D m_CapsulleCollider;
    //�˶�����
    public float moveSpeed = 1;
    public float jumpSpeed = 1;
    public int jumpTimes = 0;//�����Ծ����
    public int jumpTime;//����Ծ����
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
    //������������ʱ����ľ���ʵ��
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
    //�����Ƿ���ʾɨ����Ч
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TimeWindow" && collision.transform != transform.Find("TimeWindow"))
        {
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect",1);//��ɨ����Ч
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
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect", 0);//�ر�ɨ����Ч
        }
        InteractObject = null;
    }

}
