using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource mySource;
    public AudioClip[] clips;

    public void Start()
    {
        mySource = GetComponent<AudioSource>();
    }


    public void PlayClip(int index)
    {
        mySource.clip = clips[index];
        mySource.Play();
    }
}
