using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bellScript : Interactable
{
    public bool isPressed;
    private bool once;
    private AudioSource bellAudioSource;
    public AudioClip bellSound;

    private void Start(){
        bellAudioSource = this.GetComponent<AudioSource>();
    }

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        bellAudioSource.PlayOneShot(bellSound,0.3f);
        if(!once){
            once = true;
            isPressed = true;
        }
    }

    public override void OnLoseFocus(){

    }
}
