using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public InputAction ChangeView;//ÇÐ»»ÊÓ½Ç
    public bool isFuture = false;
    public LayerMask PastMask;
    public LayerMask FutureMask;
    public Camera mainCamera;

    public GameObject PastItem;
    public GameObject ControlObject;
    public GameObject FutureItem;
    public GameObject GlobalLight2_2;
    public GameObject SpotLight2_2L;
    public GameObject SpotLight2_2R;

    public bool IsIn2 = false;
    private void Awake()
    {
        ChangeView.performed +=
            callback =>
            {
                ViewChange();
            };
    }
    private void OnEnable()
    {
        ChangeView.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDisable()
    {
        ChangeView.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ViewChange()
    {
        if (IsIn2)
        {
            isFuture = !isFuture;
            if (isFuture)
            {
                PastItem = ControlObject.GetComponent<ControlObject>().Item;
                ControlObject.GetComponent<ControlObject>().Item = FutureItem;
                mainCamera.cullingMask = FutureMask;
                GlobalLight2_2.SetActive(false);
            }
            else
            {
                ControlObject.GetComponent<ControlObject>().Item = PastItem;
                mainCamera.cullingMask = PastMask;
                GlobalLight2_2.SetActive(true);
            }
        }
    }

}
