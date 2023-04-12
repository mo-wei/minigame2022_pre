using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class ControlObject : MonoBehaviour
{
    //InputSystem 使用C#脚本委托形式实现
    public InputAction WASD;
    public InputAction Jump;
    public InputAction Gliding;
    public InputAction MouseClick;
    public InputAction TimeWindow;
    public InputAction TimeRewind;
    public InputAction BrokeWindow;
    public InputAction Interact;

    public InputActionMap playerControl;

    public GameObject GameManager;
    public GameObject LaserShoot;

    public GameObject Item;
    public Camera mainCamera;
    public Camera timeWindowCamera;
    public GameObject timeWindowP;
    public LayerMask ClickMask;

    public float TimeWindowRadius = 4.0f;
    public List<Transform> ControlObjectsList = new List<Transform>();

    public bool isShowTimeWindow = false;
    float lastJumpTime = 0;
    public float glidingInterval = 0.1f;

    public bool isCameraLockPast = false;
    public bool isCameraLockFuture = false;

    public float Yoffset = 0f;

    public bool isHeightLimit = false;
    public bool isLimit = false;
    Vector3 CameraLockPosition;
    private void Awake()
    {
        //订阅输入事件
        Jump.performed +=
            callback =>
            {
                lastJumpTime = Time.time;
                Item.GetComponent<Player>().Jump();
            };
        MouseClick.performed +=
            callback_1 =>
            {
                if(!GameManager.GetComponent<GameManager>().isFuture)
                    ItemChange();
            };
        TimeWindow.performed +=
            callback_1 =>
            {
                if (!GameManager.GetComponent<GameManager>().isFuture)
                    CheckTimeWindow();
            };
        TimeRewind.performed +=
          callback =>
          {
              if (!GameManager.GetComponent<GameManager>().isFuture)
                  for (int i = 0; i < ControlObjectsList.Count; i++)
                  {
                        ControlObjectsList[i].GetComponent<TimeBody>().StartRewind();
                   }
          };
        BrokeWindow.performed +=
          callback =>
          {
              Item.GetComponent<Player>().Broke();
          };
        Interact.performed +=
            callback =>
            {
                Item.GetComponent<Player>().Interaction();
            };
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        WASD.Enable();
        MouseClick.Enable();
        Jump.Enable();
        TimeWindow.Enable();
        Gliding.Enable();
        TimeRewind.Enable();
        BrokeWindow.Enable();
        Interact.Enable();
    }
    private void OnDisable()
    {
        WASD.Disable();
        MouseClick.Disable();
        Jump.Disable();
        TimeWindow.Disable();
        Gliding.Disable();
        TimeRewind.Disable();
        BrokeWindow.Disable();
        Interact.Disable();
    }
    void FixedUpdate()
    {
        Item.GetComponent<Player>().Move(WASD.ReadValue<Vector2>());
        if(Gliding.ReadValue<float>() == 1 && (Time.time - lastJumpTime) > glidingInterval)
            Item.GetComponent<Player>().Gliding();
        CameraFollow();
    }
    void ItemChange()
    {
        if (isShowTimeWindow)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 mousePos3 = new Vector3(mousePos.x, mousePos.y, -10);

            Vector3 WorldPos = Camera.main.ScreenToWorldPoint(mousePos3);
            RaycastHit2D hit = Physics2D.Raycast(WorldPos, Vector2.zero, 100,ClickMask);
            WorldPos.z = 0;
            if (hit && hit.collider.tag == "ControlObjects" && (WorldPos - Item.transform.position).magnitude < TimeWindowRadius)
            {
                Item.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                Item.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                Item.GetComponent<Player>().ChangeControl();
                Item.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Item = hit.collider.gameObject;
                Item.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                Item.GetComponent<SpriteRenderer>().material.SetFloat("ScanEffect", 0);
                Item.GetComponent<Player>().ChangeControl();
                timeWindowP.transform.parent = Item.transform;
            }
        }
    }
    public void CheckTimeWindow()
    {
        isShowTimeWindow = !isShowTimeWindow;
        if (isShowTimeWindow)
        {
            if (!timeWindowP.activeInHierarchy)
            {
                timeWindowP.SetActive(true);
            }
            timeWindowP.transform.Find("TimeWindow").GetComponent<TimeWindow>().ChangeCondition(true);
        }
        else
        {
            timeWindowP.transform.Find("TimeWindow").GetComponent<TimeWindow>().ChangeCondition(false);
        }
    }
    void CameraFollow()
    {
        Vector3 Pos;
        bool isFuture = GameManager.GetComponent<GameManager>().isFuture;
        if ((isFuture && isCameraLockFuture) || (!isFuture && isCameraLockPast))
            Pos = CameraLockPosition - mainCamera.transform.position;
        else
            Pos = Item.transform.position - mainCamera.transform.position + new Vector3(0, Yoffset, 0);
        Pos.z = 0;
        if (isHeightLimit)
        {
            Pos.y = 0;
        }
        if(isLimit)
        {
            Pos.x = 0;
        }
        mainCamera.transform.position += Pos / 20;
    }
    public void CameraLock(Vector3 lockPosition)//将相机视角锁在某一点
    {
        if(GameManager.GetComponent<GameManager>().isFuture)
            isCameraLockFuture = true;
        else
            isCameraLockPast = true;
        CameraLockPosition = lockPosition;
        mainCamera.orthographicSize = 10.6f;
    }
    public void CameraUnlock()
    {
        if (GameManager.GetComponent<GameManager>().isFuture)
            isCameraLockFuture = false;
        else
            isCameraLockPast = false;
        mainCamera.orthographicSize = 10.6f;
    }
    public void ChangeTimeWindowRadius(float radius)
    {
        TimeWindowRadius = radius;
    }
}
