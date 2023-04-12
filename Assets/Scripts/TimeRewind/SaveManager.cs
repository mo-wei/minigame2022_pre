using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ܹ����������Ĵ浵��
/// </summary>
public class SaveManager : MonoBehaviour
{
    public SavePoint[] savePoints;
    private int CurrentSavepointIndex = 0;
    private void Start()
    {
        savePoints = new SavePoint[this.transform.childCount];
        for(int i = 0; i < this.transform.childCount; i++)
        {
            savePoints[i] = this.transform.GetChild(i).GetComponent<SavePoint>();
        }
        savePoints[0].gameObject.SetActive(true);
    }


    public void ChangeSavepoint()
    {
        if(CurrentSavepointIndex < savePoints.Length - 1)
        {
            savePoints[CurrentSavepointIndex].gameObject.SetActive(false);
            savePoints[++CurrentSavepointIndex].gameObject.SetActive(true);
        }
        else
        {
            //��������һ���浵��
            savePoints[CurrentSavepointIndex].gameObject.SetActive(false);
        }
    }




}
