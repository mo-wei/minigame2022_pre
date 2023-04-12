using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserShoot : MonoBehaviour
{
    public LayerMask mask;
    
    public float maxDist;
    public Vector2 ShootDirection;
    public float LaserSpeed;
    public GameObject Laser;
    Queue<GameObject> LaserLinesActive = new Queue<GameObject>();
    Queue<GameObject> LaserLinesDisactive = new Queue<GameObject>();
    bool hitObject = false;
    public bool isFlying = false;
    public bool isTurningDown = false;
    public List<Vector2> LaserPoint = new List<Vector2>();
    public Transform ShootingPosition;

    int SizeCount = -1;
    float intensity = 1;
    float width = 1;
    GameObject nowLaser;
    Vector2 flyDirection;
    Vector2 flyTo;
    Vector2 flyNow;

    float FlyTime;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            GameObject Laser_1 = Instantiate(Laser,transform);
            LaserLinesDisactive.Enqueue(Laser_1);
            Laser_1.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateLaser();
    }
    private void OnDisable()
    {
        
    }
    public void ShootLaser()
    {
        if (!isFlying && !isTurningDown)
        {
            ValueReset();
            RaycastHit2D hitInfo = Physics2D.Raycast(ShootingPosition.position, ShootDirection, 100, mask);
            LaserPoint.Add(ShootingPosition.position);
            Vector2 hitPosition = new Vector2(ShootingPosition.position.x, ShootingPosition.position.y) + ShootDirection * 20;
            Collider2D collider2D = default;
            if (hitInfo.collider != null)
            {
                hitObject = true;
                hitPosition = hitInfo.point;
                collider2D = hitInfo.collider;
            }
            else
            {
                isFlying = true;
                FlyTime = Time.time;
                LaserPoint.Add(hitPosition);
            }
            for (int i = 0; i < 20 && hitObject; i++)
            {
                LaserPoint.Add(hitPosition);
                Vector2 nextPoint = ReflectCalculate(collider2D, hitInfo.normal);
                Vector2 InNormal = hitInfo.normal;
                collider2D.gameObject.layer = 15;
                hitInfo = Physics2D.Raycast(hitPosition, nextPoint.normalized, 1000, mask);
                collider2D.gameObject.layer = 9;
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.tag == "Door")
                    {
                        hitObject = false;
                        nextPoint = hitInfo.point;
                        LaserPoint.Add(nextPoint);
                        isFlying = true;
                        FlyTime = Time.time;
                        hitInfo.collider.gameObject.GetComponent<Door>().outDoorUnlock();
                        //成功过关，此处后期可加特效/动画
                    }
                    else
                    {
                        hitPosition = hitInfo.point;
                        collider2D = hitInfo.collider;
                    }
                }
                else
                {
                    hitObject = false;
                    nextPoint = hitPosition + ReflectCalculate(collider2D, InNormal) * 5;
                    LaserPoint.Add(nextPoint);
                    isFlying = true;
                    FlyTime = Time.time;
                }
            }
        }
    }
    Vector2 ReflectCalculate(Collider2D mirror, Vector2 InNormal)
    {
        int ListSize = LaserPoint.Count;
        Vector2 InDirection = LaserPoint[ListSize - 1] - LaserPoint[ListSize - 2];
        
        return Vector2.Reflect(InDirection, InNormal);
    }
    void UpdateLaser()
    {
        if(isFlying)
        {
            if (Time.time - FlyTime > 1f)
            {
                isFlying = false;
                isTurningDown = true;
            }
            if (SizeCount + 1 == LaserPoint.Count)
            { 
                isFlying = false;
                isTurningDown = true;
            }
            else
            {
                if (SizeCount == -1)
                {
                    SizeCount += 1;
                    FlyTime = Time.time;
                    nowLaser = GetLaser();
                    nowLaser.GetComponent<LineRenderer>().SetPosition(0, LaserPoint[SizeCount]);
                    flyNow = LaserPoint[SizeCount];
                }
                flyDirection = (LaserPoint[SizeCount + 1] - LaserPoint[SizeCount]).normalized;
                if ((flyDirection * LaserSpeed).magnitude > (LaserPoint[SizeCount + 1] - flyNow).magnitude)
                {
                    SizeCount += 1;
                    FlyTime = Time.time;
                    if (SizeCount + 1 == LaserPoint.Count)
                    {
                        isFlying = false;
                        isTurningDown = true;
                        return;
                    }
                    else
                    {
                        flyTo = LaserPoint[SizeCount];
                        nowLaser.GetComponent<LineRenderer>().SetPosition(1, flyTo);
                        nowLaser = GetLaser();
                        nowLaser.GetComponent<LineRenderer>().SetPosition(0, LaserPoint[SizeCount]);
                        flyDirection = (LaserPoint[SizeCount + 1] - LaserPoint[SizeCount]).normalized;
                        flyNow = LaserPoint[SizeCount];
                        if (SizeCount == LaserPoint.Count)
                        {
                            isFlying = false;
                            isTurningDown = true;
                        }
                    }
                }
                flyTo = flyNow + flyDirection * LaserSpeed;
                flyNow = flyTo;
                nowLaser.GetComponent<LineRenderer>().SetPosition(1, flyTo);
            }
        }
        if (isTurningDown)
        {
            intensity -= 2f * Time.deltaTime;
            width += 4f * Time.deltaTime;
            for (int i = 0; i < LaserLinesActive.Count; i++)
            {
                GameObject newLaser = LaserLinesActive.Dequeue();
                newLaser.GetComponent<LineRenderer>().material.SetFloat("Intensity", intensity);
                newLaser.GetComponent<LineRenderer>().material.SetFloat("Width", width);
                LaserLinesActive.Enqueue(newLaser);
                
            }
            if(intensity < 0.1f)
            {
                isTurningDown = false;
                int Count = LaserLinesActive.Count;
                for (int i = 0; i < Count; i++)
                {
                    GameObject newLaser = LaserLinesActive.Dequeue();
                    CloseLaser(newLaser);
                }
                ValueReset();
            }
            
        }
    }
    GameObject GetLaser()
    {
        GameObject newLaser = LaserLinesDisactive.Dequeue();
        newLaser.SetActive(true);
        LaserLinesActive.Enqueue(newLaser);
        return newLaser;
    }
    void CloseLaser(GameObject laser)
    {
        LaserLinesDisactive.Enqueue(laser);
        laser.GetComponent<LineRenderer>().material.SetFloat("Intensity", 1);
        laser.GetComponent<LineRenderer>().material.SetFloat("Width", 1);
        laser.SetActive(false);
    }
    private void ValueReset()
    {
        LaserPoint.Clear();
        SizeCount = -1;
        intensity = 1;
        width = 1;

    }
}
