using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWindow : MonoBehaviour
{
    float spriteScale = 10.0f;
    public GameObject controlObject;
    public GameObject GameManager;
    bool isOpen = false;
    public GameObject quad;
    // Start is called before the first frame update
    void Start()
    {
        spriteScale = 10.0f / 2.0f * controlObject.GetComponent<ControlObject>().TimeWindowRadius;
    }
    private void OnEnable()
    {
        transform.parent.localPosition = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        spriteScale = 10.0f / 2.0f * controlObject.GetComponent<ControlObject>().TimeWindowRadius;
        if (GameManager.GetComponent<GameManager>().isFuture)
        {
            isOpen = false;
            controlObject.GetComponent<ControlObject>().isShowTimeWindow = false;
        }
        WindowMove();
        OpenWindow();
        CloseWindow();
    }
    public void ChangeCondition(bool condition)
    {
        isOpen = condition;
    }
    void WindowMove()
    {
        if(transform.parent.localPosition.magnitude > 0.1f)
        {
            Vector3 Pos = new Vector3(0, 0, 0) - transform.parent.localPosition;
            Pos.z = 0;
            transform.parent.localPosition += Pos / 20;
        }
    }
    void OpenWindow()
    {
        if (isOpen)
        {
            float scale = spriteScale / transform.parent.parent.localScale.x;
            if (transform.localScale.x < 0.95f * scale / transform.parent.localScale.x)
            {
                transform.localScale = new Vector3(transform.localScale.x * 19.0f / 20.0f + scale / transform.parent.parent.localScale.x / transform.parent.parent.parent.localScale.x / 20.0f, transform.localScale.y * 19.0f / 20.0f + scale / transform.parent.parent.localScale.y / transform.parent.parent.parent.localScale.y / 20.0f, transform.localScale.z * 19.0f / 20.0f + scale / transform.parent.parent.localScale.z / transform.parent.parent.parent.localScale.z / 20.0f);
                float aimedScale = spriteScale / transform.parent.parent.localScale.x / transform.parent.localScale.x;
                quad.GetComponent<MeshRenderer>().material.SetFloat("Radius", 0.38f / 8.0f * controlObject.GetComponent<ControlObject>().TimeWindowRadius * transform.localScale.x / aimedScale);
            }
        }
    }
    void CloseWindow()
    {
        if (!isOpen)
        {
            float scale = spriteScale / transform.parent.parent.localScale.x;
            if (transform.localScale.x > 3f)
            {
                transform.localScale = new Vector3(transform.localScale.x * 15.0f / 20.0f + 5.0f / transform.parent.parent.localScale.x / transform.parent.parent.parent.localScale.x / 20.0f, transform.localScale.y * 15.0f / 20.0f + 5.0f / transform.parent.parent.localScale.y / transform.parent.parent.parent.localScale.y / 20.0f, transform.localScale.z * 15.0f / 20.0f + 5.0f / transform.parent.parent.localScale.z / transform.parent.parent.parent.localScale.z / 20.0f);
                float aimedScale = spriteScale / transform.parent.parent.localScale.x / transform.parent.parent.parent.localScale.x;
                quad.GetComponent<MeshRenderer>().material.SetFloat("Radius", 0.38f / 8.0f * controlObject.GetComponent<ControlObject>().TimeWindowRadius * transform.localScale.x / aimedScale);
            }
            else
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
