using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsSimple : MonoBehaviour
{
    bool isEnter = false;
    public GameObject TipItem;
    public float Speed;
    public float Amp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Float();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnter && collision.gameObject.tag == "ControlObjects")
        {
            if (collision.gameObject.layer == 7)
            {
                TipItem.SetActive(true);
            }
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isEnter && collision.gameObject.tag == "ControlObjects")
        {
            TipItem.SetActive(false);
            isEnter = false;
        }
    }
    void Float()
    {
        if (TipItem.activeInHierarchy)
        {
            Vector3 pos = new Vector3(0, Amp * Mathf.Sin(Speed * Time.time) * Time.deltaTime);
            TipItem.transform.position += pos;
        }
    }
}
