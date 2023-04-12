using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

//���ڴ��������λ���Լ���ת�Ƕ�
public class TimePoint
{
    public Vector3 _position;
    public Quaternion _rotation;
    //�˴������ԼӶ���֮֡��ģ�������Ҫ
    public TimePoint(Vector3 position, Quaternion rotation)
    {
        _position = position;
        _rotation = rotation;
    }
}
public class TimeBody : MonoBehaviour
{
    public bool isRewinding;
    private List<TimePoint> timePoints;
    private Rigidbody2D rb;
    private float grivaty;
    [Range(0, 5)]
    //�����������һ���浵��Ĵ浵������ʵʱ�ĸ����ҵĻ����ٶ�,�൱�����Ŷ���
    private int rewindSpeed = 2;

    private void Awake()
    {
        
    }
    private void Start()
    {
        timePoints = new List<TimePoint>();
        rb = GetComponent<Rigidbody2D>();
        grivaty = rb.gravityScale;
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();

    }
    //��¼
    private void Record()
    {
        if (timePoints.Count == 0 || Vector3.Distance(this.transform.position, timePoints[0]._position) > 0.1f)
        {
            timePoints.Insert(0, new TimePoint(this.transform.position, this.transform.rotation));
        }

    }
    //����
    private void Rewind()
    {
        if (timePoints.Count > rewindSpeed)
        {
            transform.position = timePoints[0]._position;
            timePoints.RemoveRange(0, rewindSpeed);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.gravityScale = 0f;
        rb.isKinematic = true;
    }

    private void StopRewind()
    {
        isRewinding = false;
        rb.gravityScale = grivaty;
        rb.isKinematic = false;
    }

    //��մ�����е�
    public void ClearAllRecord()
    {
        timePoints.Clear();
    }

}
