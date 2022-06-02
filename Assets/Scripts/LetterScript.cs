using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : Interactable
{

    [SerializeField] private GameObject letterCanvas;
    public bool interacted;

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        interacted = true;
        letterCanvas.SetActive(true);
    }

    public override void OnLoseFocus(){

    }
}
