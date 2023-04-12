using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : Player
{
    float alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LeaveControl();
    }
    public override void Move(Vector2 data)
    {
        m_rigidbody.velocity = new Vector2(data.x * moveSpeed, m_rigidbody.velocity.y);
    }
    public override void ChangeControl()
    {
        isControl = !isControl;
        if (isControl)
        {
            
        }
        else
        {
            
        }
    }
    void LeaveControl()
    {
        if(!isControl)
        {
            alpha -= Time.deltaTime * 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
            if(alpha < 0.2f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
