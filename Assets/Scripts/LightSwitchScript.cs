using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : Interactable
{   

    [SerializeField] private bool lightState;
    [SerializeField] private GameObject[] allLights;
    public bool interacted;
    private AudioSource switchAudioSource;
    public AudioClip lightSwitchSound;

    private void Start(){
        lightState = false;
        interacted = false;
        switchAudioSource = this.GetComponent<AudioSource>();
    }

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        switchAudioSource.PlayOneShot(lightSwitchSound,0.5f);
        interacted = true;
        lightState = !lightState;

        if(lightState){
            turnOn();
        }
        else{
            turnOff();
        }
        
    }

    public override void OnLoseFocus(){

    }

    private void turnOn(){
        foreach(GameObject obj in allLights){
            obj.SetActive(true);
        }
    }

    private void turnOff(){
        foreach(GameObject obj in allLights){
            obj.SetActive(false);
        }
    }
}
