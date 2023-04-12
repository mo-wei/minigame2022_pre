using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Player
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Broke()
    {
        //后期可以加特效
        if (gameObject.layer == 9)
        {
            gameObject.layer = 15;
            image.GetComponent<SpriteRenderer>().material.SetFloat("Alpha", 0.5f);
        }
        else
        {
            gameObject.layer = 9;
            image.GetComponent<SpriteRenderer>().material.SetFloat("Alpha", 1f);
        }
    }
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TimeWindow" && collision.transform != transform.Find("TimeWindow"))
        {
            image.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect", 1);//打开扫光特效
        }
    }
    private new void OnTriggerStay2D(Collider2D collision)
    {
        InteractObject = collision;
    }
    protected new void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TimeWindow" && collision.transform != transform.Find("TimeWindow"))
        {
            image.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect", 0);//关闭扫光特效
        }
        InteractObject = null;
    }
}
