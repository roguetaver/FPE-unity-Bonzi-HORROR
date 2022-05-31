using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bellScript : Interactable
{
    public bool isPressed;
    private bool once;

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        if(!once){
            once = true;
            isPressed = true;
        }
    }

    public override void OnLoseFocus(){

    }
}
