using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextType : MonoBehaviour
{
    public float charsPerSecond = 0.2f;//����ʱ����
    private string words;//������Ҫ��ʾ������

    private bool isActive = false;
    private bool isPanelActive = false;
    private bool isPanelClose = false;
    private bool isPanelWork = false;
    private float timer;//��ʱ��
    private float alpha = 1;

    public InputAction Skip;
    public float TurningSpeed = 1.0f;

    public Text myText;
    private int currentPos = 0;//��ǰ����λ��

    private int PressCount = 0;
    public List<string> WordsList = new List<string>();

    private int WordNumber;
    private void Awake()
    {
        WordsList.Add("�˵�һ�����ж����ź���\n�������õ���ͱ���̤��ս���뿪����ˣ�\n����԰׵���ֽ��д�����ĸ�ף�\n������������̵ľ��������Ļ��ᡣ\n��Щ�ź����������������Ŷ���ʧ�������Щ�ź�����ʱ�����žִ��ڵ����塣\n" +
            "������һ�Σ���������������ʹ\n����ס���������������ֻ�ܸ�������ǰ����\n���ڻص����ǵļң�ȥ�����ɡ�");
        WordsList.Add("�����ļұ�ս��ϴ�٣�������һ�ˣ��������Ѿ��������޴���Ѱ����\n�㻹�С�ʱ�䡱����Ȼ��ȥ�Ѳ���Ѱ����Ϊʲô��ȥδ�������أ�");
        WordsList.Add("��һ�ݳٵ��˼�ʮ���˼������Բ����Ըһ�е��ź����ڽ��������ж��ܺͽ⡣");
        Skip.performed += 
         callback =>
        {
            SkipContent();
        };
    }
    private void OnEnable()
    {
        Skip.Enable();
    }
    private void OnDisable()
    {
        Skip.Disable();
    }
    // Use this for initialization
    void Start()
    {
        timer = 0;
        words = "";
        myText.text = "";//��ȡText���ı���Ϣ�����浽words�У�Ȼ��̬�����ı���ʾ���ݣ�ʵ�ִ��ֻ���Ч��
        ShowText(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PanelAlphaChange();
        OnStartWriter();
        PanelClose();
    }

    public void StartEffect()
    {
        isActive = true;
    }
    /// <summary>
    /// ִ�д�������
    /// </summary>
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {//�жϼ�ʱ��ʱ���Ƿ񵽴�
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//ˢ���ı���ʾ����

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// �������֣���ʼ������
    /// </summary>
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
        StartCoroutine("TextStay");
    }
    public void ShowText(int number)
    {
        words = WordsList[number];
        WordNumber = number;
        isPanelActive = true;
    }
    void PanelAlphaChange()
    {
        if (isPanelActive)
        {
            if (WordNumber == 0)
            {
                alpha += TurningSpeed * Time.deltaTime;
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                if (alpha > 0.95f + 1)
                {
                    gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                    isPanelActive = false;
                    isActive = true;
                    isPanelWork = true;
                    myText.GetComponent<Text>().color = new Color(255, 255, 255, 1);
                    alpha = 1;
                }
            }
            else
            {
                alpha += TurningSpeed * Time.deltaTime;
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, alpha - 1);
                if (alpha > 0.95f + 1)
                {
                    gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                    isPanelActive = false;
                    isActive = true;
                    isPanelWork = true;
                    myText.GetComponent<Text>().color = new Color(255, 255, 255, 1);
                    alpha = 1;
                }
            }
        }
    }
    void PanelClose()
    {
        if(isPanelClose)
        {
            if (WordNumber == 2)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                alpha -= TurningSpeed * Time.deltaTime;
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
                myText.GetComponent<Text>().color = new Color(255, 255, 255, alpha);
                if (alpha < 0.1f)
                {
                    gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    myText.GetComponent<Text>().color = new Color(255, 255, 255, 0);
                    myText.GetComponent<Text>().text = "";
                    isPanelClose = false;
                    alpha = 1;
                }
            }
        }
    }
    private IEnumerator TextStay()
    {
        yield return new WaitForSeconds(2f);

        if (isPanelWork)
        {
            isPanelClose = true;
            isPanelWork = false;
        }
    }
    void SkipContent()
    {
        if (isPanelWork)
        {
            PressCount += 1;
            if (isActive)
            {
                if (PressCount == 1)
                {
                    OnFinish();
                }

            }
            if (PressCount == 2)
            {
                isPanelClose = true;
                isPanelWork = false;
                PressCount = 0;
            }
        }
    }
}
