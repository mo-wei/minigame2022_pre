using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightLimit : MonoBehaviour
{
    public GameObject ControlObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "ControlObjects")
            ControlObject.GetComponent<ControlObject>().isHeightLimit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ControlObjects")
            ControlObject.GetComponent<ControlObject>().isHeightLimit = false;
    }
}
