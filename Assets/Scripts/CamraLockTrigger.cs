using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraLockTrigger : MonoBehaviour
{
    public GameObject ControlObject;
    public Transform CameraLockPosition;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive && (collision.gameObject.tag == "ControlObjects" || collision.gameObject.tag == "Wife"))
        {
            Debug.Log("work");
            ControlObject.GetComponent<ControlObject>().CameraLock(CameraLockPosition.position);
            isActive = false;
        }
    }
}
