using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : Interactable
{
    public bool interacted;
    [SerializeField] private GameManager gameManager;
    private playerTalkingScript playerDialog;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog").GetComponent<playerTalkingScript>();   
    }

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        if(gameManager.estadoAtual < 3){
            playerDialog.SetDialog(" No need to go to my car now ", 4f);
        }
        else{
            interacted = true;
        }
        
    }

    public override void OnLoseFocus(){

    }
}
