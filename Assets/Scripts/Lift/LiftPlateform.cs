using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPlateform : MonoBehaviour
{
    //通过放缩图片达到升降的效果
    private Transform top;
    private Transform middle;
    private Transform below;
    public float xMultiple;
    public float yMultiple;
    public float initXScale;
    public float initYScale;
    private float belowToMiddle;
    private float middleTotop;

    private IEnumerator Domovement;
    //对应的按钮
    private void Start()
    {
        //分别得到三个部件的初始位置
        top = this.transform.GetChild(0);
        middle = this.transform.GetChild(1);
        below = this.transform.GetChild(2);
        initXScale = middle.localScale.x;
        initYScale = middle.localScale.y;
        

        belowToMiddle = middle.localPosition.y - below.localPosition.y;
        middleTotop = top.localPosition.y - middle.localPosition.y;
    }

    public void UpMovement()
    {
            if (Domovement != null)
            {
                StopCoroutine(Domovement);
            }
            Domovement = Movement(xMultiple, yMultiple);
            StartCoroutine(Domovement);
    }

    public void DownMovement()
    {
        if (Domovement != null)
        {
            StopCoroutine(Domovement);
        }
        Domovement = Movement(initYScale, initYScale);
        StartCoroutine(Domovement);
    }

    private IEnumerator Movement(float xMultiple_target, float yMultiple_target)
    {
        float xMultiple_now = 0, yMultiple_now = 0;
        while(Mathf.Abs(middle.localScale.x - xMultiple_target) > 0.01f || Mathf.Abs(middle.localScale.y - yMultiple_target) > 0.01f)
        {
            yield return null;
            xMultiple_now =  Mathf.Lerp(middle.localScale.x, xMultiple_target, Time.deltaTime * 5 );
            yMultiple_now = Mathf.Lerp(middle.localScale.y, yMultiple_target, Time.deltaTime * 5);
            middle.localScale = new Vector3(xMultiple_now, yMultiple_now, middle.lossyScale.z);
            middle.localPosition = new Vector3(middle.localPosition.x, below.localPosition.y + belowToMiddle * yMultiple_now, middle.localPosition.z);
            top.localPosition = new Vector3(top.localPosition.x, middle.localPosition.y + middleTotop * yMultiple_now, top.localPosition.z);
        }

    }
}
