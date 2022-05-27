using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestInteractable : Interactable
{
    public override void OnFocus(){
        print("looking at :" + gameObject.name);
    }

    public override void OnInteract(){
        print("interacted with :" + gameObject.name);
    }

    public override void OnLoseFocus(){
        print("stopped looking at :" + gameObject.name);
    }
}