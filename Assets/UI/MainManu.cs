using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

   public void Begin()
    {
        SceneManager.LoadScene(1);
    } 




}
