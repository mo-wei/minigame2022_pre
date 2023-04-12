using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Player
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Move(Vector2 data)
    {
        m_rigidbody.velocity = new Vector2(data.x * moveSpeed, m_rigidbody.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
