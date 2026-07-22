using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Input = UnityEngine.Windows.Input;
using Random = UnityEngine.Random;

public class Audio : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        EventManager.OnAudioRequestEvent += ClipSetUp;
    }


    private void ClipSetUp(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + clipName);
       
        if (clip != null)
        {
            Debug.Log("Playing: " + clipName);
            audioSource.clip = clip;
            PlayClip();
        }

        
    }

    private void PlayClip()
    {
        audioSource.pitch = Random.Range(1f, 1.5f);
        audioSource.Play();
    }


    private void OnDisable()
    {
        EventManager.OnAudioRequestEvent -= ClipSetUp;
    }
}