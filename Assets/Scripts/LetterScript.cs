using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : Interactable
{

    [SerializeField] private GameObject letterCanvas;

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        letterCanvas.SetActive(true);
    }

    public override void OnLoseFocus(){

    }
}
