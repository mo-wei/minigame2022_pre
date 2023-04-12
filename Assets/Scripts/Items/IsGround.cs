using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    //这个脚本用来判断物体是否着地

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "tilemap")
        {
            print(1);
            transform.parent.GetComponent<Player>().isGround = true;
            transform.parent.GetComponent<Player>().jumpTime = transform.parent.GetComponent<Player>().jumpTimes;
            this.transform.parent.GetComponent<Animator>().SetBool("IsJump", false);
            this.transform.parent.GetComponent<Animator>().SetBool("IsFly", false);
        }
    }
}
