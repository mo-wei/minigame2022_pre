using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Book : MonoBehaviour
{
    private Transform[] pages;
    private int pagesCount;
    private int nowPage = 0;


    private void Start()
    {
        pagesCount = this.transform.childCount;
        pages = new Transform[pagesCount];
        for(int i = 0; i < pagesCount; i++)
        {
            pages[i] = this.transform.GetChild(i);
        }
        pages[0].gameObject.SetActive(true);

    }

    public void ToNextPage()
    {
        pages[nowPage].gameObject.SetActive(false);
        nowPage++;
        pages[nowPage].gameObject.SetActive(true);
    }
    public void ToLastPage()
    {
        pages[nowPage].gameObject.SetActive(false);
        nowPage--;
        pages[nowPage].gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        //�˴����俴���������Ӧ����
    }

    public void Close()
    {
        this.transform.gameObject.SetActive(!this.transform.gameObject.activeSelf);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
