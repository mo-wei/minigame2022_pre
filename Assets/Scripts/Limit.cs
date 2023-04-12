using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public GameObject ControlObject;
    public GameObject GameManager;
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
        if (GameManager.GetComponent<GameManager>().IsIn2 &&( collision.tag == "ControlObjects" || collision.tag == "Wife"))
            ControlObject.GetComponent<ControlObject>().isLimit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.GetComponent<GameManager>().IsIn2 && (collision.tag == "ControlObjects" || collision.tag == "Wife"))
            ControlObject.GetComponent<ControlObject>().isLimit = false;
    }
}
