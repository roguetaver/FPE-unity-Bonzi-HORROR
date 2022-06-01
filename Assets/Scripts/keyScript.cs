using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : Interactable
{
    public bool interacted;
    private bool once;

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        if(!once){
            once = true;
            interacted = true;
        }
    }

    public override void OnLoseFocus(){

    }
}
