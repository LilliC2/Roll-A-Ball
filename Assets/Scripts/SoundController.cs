using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip winSound;
    public AudioClip timerSound;
    public AudioClip boostPadSound;
    public AudioClip jumpPadSound;
    
    
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }

    public void PlayTimerSound()
    {
        PlaySound(timerSound);
    }

    public void PlayBoostPadSound()
    {
        PlaySound(boostPadSound);
    }

    public void PlayJumpPadSound()
    {
        PlaySound(jumpPadSound);
    }

    void PlaySound(AudioClip _newSound)
    {
        //set the audio sources audioclip to be passed in sound
        audioSource.clip = _newSound;
        //play audiio source
        audioSource.Play();
    }

    public void PlayCollisionSound(GameObject _go)
    {
        //check to see eif the collided object has an audio source
        //this is a failwsafe in case we forget to attach one to our wall
        if(_go.GetComponent<AudioSource>() != null)
        {
            //play audio on the wall object
            _go.GetComponent<AudioSource>().Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
