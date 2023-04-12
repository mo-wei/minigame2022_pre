using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseManu : MonoBehaviour
{
    public GameObject pauseManu;
    public AudioMixer audioMixer;
    public GameObject book;
    public InputAction pause;

    private void Awake()
    {
        //¶©ÔÄÊäÈëÊÂ¼þ
        pause.performed +=
            callback =>
            {
                ChangePauseState();
            };
    }

    private void OnEnable()
    {
        pause.Enable();
    }
    private void OnDisable()
    {
        pause.Enable();
    }

    public void ChangePauseState()
    {
        print(1);
        pauseManu.SetActive(!pauseManu.activeSelf);
        if (pauseManu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            if(book.activeSelf)
            {
                book.SetActive(false);
            }
        }
    }

    public void ReturnMainManu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void AudioControl(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }


    public void ChangeBookState()
    {
        book.SetActive(!book.activeSelf);
    }
}
