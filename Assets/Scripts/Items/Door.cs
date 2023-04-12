using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Door : MonoBehaviour
{
    public bool isIn = true;
    bool HideForwardFuture = false;
    bool HideForwardPast = false;
    bool RecoverForwardFuture = false;
    bool RecoverForwardPast = false;
    public float alphaP = 1;
    public float alphaF = 1;

    public GameObject ControlObject;
    public Transform CameraPosition;
    public GameObject PastForward;
    public GameObject FutureForward;
    public GameObject wall;
    public GameObject LightLeft;

    public Sprite DoorOpen;
    public Sprite DoorClose;

    public bool InsidePast = false;
    public bool InsideFuture = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HiddingForward();
        RecoverForward();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wife")
        {
            if (!InsideFuture)
            {
                ControlObject.GetComponent<ControlObject>().CameraLock(CameraPosition.position);
                HideForwardFuture = true;
                HideForwardPast = true;
                InsideFuture = true;
            }
            else
            {
                ControlObject.GetComponent<ControlObject>().CameraUnlock();
                RecoverForwardFuture = true;
                FutureForward.SetActive(true);
                InsideFuture = false;
            }
        }
        else if (collision.gameObject.tag == "ControlObjects")
        {
            if (!InsidePast)
            {
                ControlObject.GetComponent<ControlObject>().CameraLock(CameraPosition.position);
                HideForwardFuture = true;
                HideForwardPast = true;
                ControlObject.GetComponent<ControlObject>().ChangeTimeWindowRadius(8.0f);
                InsidePast = true;
            }
            else
            {
                ControlObject.GetComponent<ControlObject>().CameraUnlock();
                RecoverForwardPast = true;
                ControlObject.GetComponent<ControlObject>().ChangeTimeWindowRadius(14.0f);
                PastForward.SetActive(true);
                InsidePast = false;
            }
        }
    }
    //成功过关
    public void outDoorUnlock()
    {
        AudioManager.instance.DoorAudio();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = DoorOpen;
        LightLeft.GetComponent<Light2D>().intensity = 1;
        LightLeft.GetComponent<Light2D>().pointLightOuterRadius = 23;
        wall.SetActive(false);
    }
    void HiddingForward()
    {
        if(HideForwardFuture)
        {
            alphaF -= 2.0f * Time.deltaTime;
            FutureForward.GetComponent<SpriteRenderer>().material.SetFloat("Alpha", alphaF);
            if(alphaF < 0.3f)
            {
                FutureForward.SetActive(false);
                alphaF = 1f;
                HideForwardFuture = false;
            }
        }
        if (HideForwardPast)
        {
            alphaP -= 2.0f * Time.deltaTime;
            PastForward.GetComponent<SpriteRenderer>().material.SetFloat("Alpha", alphaP);
            if (alphaP < 0.3f)
            {
                PastForward.SetActive(false);
                alphaP = 1f;
                HideForwardPast = false;
            }
        }
    }
    void RecoverForward()
    {
        if (RecoverForwardFuture)
        {
            alphaF += 2.0f * Time.deltaTime;
            FutureForward.GetComponent<SpriteRenderer>().material.SetFloat("Alpha", alphaF - 1);
            if (alphaF - 1 > 0.99f)
            {
                alphaF = 1f;
                RecoverForwardFuture = false;
            }
        }
        if (RecoverForwardPast)
        {
            alphaP += 2.0f * Time.deltaTime;
            PastForward.GetComponent<SpriteRenderer>().material.SetFloat("Alpha", alphaP - 1);
            if (alphaP - 1 > 0.99f)
            {
                alphaP = 1f;
                RecoverForwardPast = false;
            }
        }
    }
}
