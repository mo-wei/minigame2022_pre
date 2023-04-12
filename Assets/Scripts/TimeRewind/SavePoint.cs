using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public bool isBeginSave = false;
    private SaveManager saveManager;
    public Transform[] saveObjects;

    //用于储存孩子的数量
    private int saveObjectCoune;
    private void Awake()
    {
        saveObjectCoune = saveObjects.Length;
        saveManager = this.transform.parent.GetComponent<SaveManager>();
    }



    private void OnEnable()
    {
        for(int i = 0; i < saveObjectCoune; i++)
        {
            saveObjects[i].gameObject.GetComponent<TimeBody>().enabled = true;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < saveObjectCoune; i++)
        {
            if(saveObjects[i].gameObject.transform)
                saveObjects[i].gameObject.GetComponent<TimeBody>().enabled = false;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ControlObjects"))
        {
            collision.gameObject.GetComponent<TimeBody>().ClearAllRecord();
            saveManager.ChangeSavepoint();
        }
    }
}
