using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

//用于储存物体的位置以及旋转角度
public class TimePoint
{
    public Vector3 _position;
    public Quaternion _rotation;
    //此处还可以加动画帧之类的，如有需要
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
    //根据玩家与上一个存档点的存档点数来实时的更改我的回溯速度,相当于跳着读点
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
    //记录
    private void Record()
    {
        if (timePoints.Count == 0 || Vector3.Distance(this.transform.position, timePoints[0]._position) > 0.1f)
        {
            timePoints.Insert(0, new TimePoint(this.transform.position, this.transform.rotation));
        }

    }
    //回溯
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

    //清空存的所有点
    public void ClearAllRecord()
    {
        timePoints.Clear();
    }

}
