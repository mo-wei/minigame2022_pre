using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform up;
    float UpY,ButtonY;
    float originPosition;
    public float Speed;
    //private Collider2D coll;
    private Rigidbody2D rb;
    public bool IsUp;
    public float waitTime = 0.2f;

    bool isPress = false;
    float leaveTime = 0;
    public LiftPlateform liftPlateform;


    // Start is called before the first frame update
    void Start()
    {
        originPosition = gameObject.transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        UpY = up.position.y;
        ButtonY=rb.position.y;
        Destroy(up.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ButtonUp();
    }

    //让按钮一直往上，直到Up点
    public void ButtonUp()
    {
        if (transform.position.y < originPosition - 0.01f)
        { 
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.position = new Vector3(transform.position.x, originPosition,transform.position.z);
        }
        //Debug.Log("Up的Y"+UpY);
        //Debug.Log("按钮的Y"+ButtonY);
        if (IsUp)
        {
            if (!isPress && (Time.time - leaveTime) > waitTime)
            {
                rb.velocity = new Vector2(0, Speed);
                if (rb.transform.position.y > UpY)
                {
                    AudioManager.instance.UpAndDownAudio();
                    IsUp = false;
                    liftPlateform.DownMovement();
                }
            }
        }
            
        else
        {
            rb.velocity = Vector2.zero;
            if (rb.transform.position.y < UpY)
            {
                AudioManager.instance.UpAndDownAudio();
                IsUp = true;
                liftPlateform.UpMovement();
            }
        }
        
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "ControlObjects")
        {
            isPress = !isPress;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "ControlObjects")
        {
            rb.velocity = new Vector2(0, 0);
            isPress = !isPress;
            leaveTime = Time.time;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
    }
}
