using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource audioSource;
    public static BGM instance;
    public AudioClip bgm1, bgm2;

    private void Start()
    {
        if (instance == null)
        {
            instance = this.GetComponent<BGM>();
        }
        audioSource = GetComponent<AudioSource>();
    }
    public void BGM1()
    {
        audioSource.clip = bgm1;
        audioSource.Play();
    }
    public void BGM2()
    {
        audioSource.clip = bgm2;
        audioSource.Play();
    }
}
