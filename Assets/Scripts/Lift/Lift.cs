using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public Transform up,down;
    public Transform controlObject;
    float UpY,DownY;
    public float Speed;
    bool isUp = false;
    // Start is called before the first frame update
    void Start()
    {
        UpY=up.position.y;
        DownY=down.position.y;
        Destroy(up.gameObject);
        Destroy(down.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        var button1 = GameObject.Find("Button1");
        var button = button1.GetComponent<Button>();
        if (button.IsUp)
        {
            if (transform.position.y > UpY)
            {
                transform.position = new Vector3(transform.position.x, UpY, transform.position.z);
                isUp = false;
            }
            else if(transform.position.y < UpY)
            {
                transform.position += new Vector3(0, Speed * Time.deltaTime, 0);
                isUp = true;
            }

        }
        else
        {
            if (transform.position.y < DownY)
            {
                transform.position = new Vector3(transform.position.x, DownY, transform.position.z);
            }
            else if(transform.position.y > DownY)
            {
                transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = controlObject;
    }
}
