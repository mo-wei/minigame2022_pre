using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tips : MonoBehaviour
{
    public GameObject ControlObject;
    public GameObject tipQ;
    public GameObject tipE;
    public GameObject tipEfemale;
    public GameObject FutureBackground;
    public InputAction TipShow;
    public InputAction LetterPut;
    public GameObject panel;

    public Sprite FutureLetterScene;

    bool changeTip = false;
    public bool PressQ = false;
    bool isLetterPut = false;
    bool isEnter = false;

    public float Speed;
    public float Amp;

    Collider2D CollisionCharacter;
    // Start is called before the first frame update
    private void Awake()
    {
        LetterPut.performed += PutLetter;
    }
    private void OnEnable()
    {
        TipShow.Enable();
        LetterPut.Enable();
    }
    private void OnDisable()
    {
        TipShow.Disable();
        LetterPut.Disable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowTip();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionCharacter = collision;
        if (!isLetterPut && !isEnter && collision.gameObject.tag == "ControlObjects")
        {
            if (collision.gameObject.layer == 7)
            {
                tipQ.SetActive(true);
            }
            isEnter = true;
        }
        if(isLetterPut && collision.gameObject.tag == "Wife")
        {
            tipEfemale.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CollisionCharacter = null;
        if (!isLetterPut && collision.gameObject.tag == "ControlObjects")
        {
            print("work");
            tipQ.SetActive(false);
            tipE.SetActive(false);
            isEnter = false;
        }
        if (isLetterPut && collision.gameObject.tag == "Wife")
        {
            tipEfemale.SetActive(false);
        }
    }
    void PutLetter(InputAction.CallbackContext callbackContext)
    {
        if (PressQ && !isLetterPut)
        {
            Debug.Log("put letter");
            FutureBackground.GetComponent<SpriteRenderer>().sprite = FutureLetterScene;
            isLetterPut = true;
            tipQ.SetActive(false);
            tipE.SetActive(false);
        }
        if(isLetterPut && CollisionCharacter.tag == "Wife")
        {
            panel.GetComponent<TextType>().ShowText(2);
            //此处结束游戏
        }
    }
    public void ShowTip()
    {
        if (isEnter && !isLetterPut)
        {
            if (TipShow.ReadValue<float>() == 1)
            {
                tipQ.SetActive(false);
                tipE.SetActive(true);
                PressQ = true;
            }
            else
            {
                tipQ.SetActive(true);
                tipE.SetActive(false);
                PressQ = false;
            }
        }
    }
    void Float()
    {
        if (tipQ.activeInHierarchy)
        {
            Vector3 pos = new Vector3(0, Amp * Mathf.Sin(Speed * Time.time) * Time.deltaTime);
            tipQ.transform.position += pos;
        }
        if (tipE.activeInHierarchy)
        {
            Vector3 pos = new Vector3(0, Amp * Mathf.Sin(Speed * Time.time) * Time.deltaTime);
            tipE.transform.position += pos;
        }
        if (tipEfemale.activeInHierarchy)
        {
            Vector3 pos = new Vector3(0, Amp * Mathf.Sin(Speed * Time.time) * Time.deltaTime);
            tipEfemale.transform.position += pos;
        }
    }
}
