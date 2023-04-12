using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÉùÒô¿ØÖÆÆ÷
/// </summary>
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioManager instance;
    public AudioClip doorAduio, lightAudio, upAndDownAudio;
    private int upAndDonwCount;

    private void Start()
    {
        upAndDonwCount = 0;
        if(instance == null)
        {
            instance = this.GetComponent<AudioManager>();
        }
        audioSource = GetComponent<AudioSource>();
    }
    public void DoorAudio()
    {
        audioSource.clip = doorAduio;
        audioSource.Play();
    }
    public void LightAudio()
    {
        audioSource.clip = lightAudio;
        audioSource.Play();
    }
    public void UpAndDownAudio()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        if (upAndDonwCount < 2)
        {
            upAndDonwCount++;
        }
        else
        {
            audioSource.clip = upAndDownAudio;
            audioSource.Play();
        }
    }
}