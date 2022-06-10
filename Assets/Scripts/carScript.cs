using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : Interactable
{
    public bool interacted;

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        interacted = true;
    }

    public override void OnLoseFocus(){

    }
}
