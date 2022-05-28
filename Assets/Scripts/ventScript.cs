using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventScript : Interactable
{
    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        this.GetComponent<Animator>().SetTrigger("OpenVent");
    }

    public override void OnLoseFocus(){
        
    }
}
