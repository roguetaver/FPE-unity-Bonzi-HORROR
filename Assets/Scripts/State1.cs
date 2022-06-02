using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1 : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDone;
    private playerTalkingScript playerDialog;
    private GameObject music;
    [SerializeField] private GameObject bell;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject room1door;


    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog").GetComponent<playerTalkingScript>();      
        gameManager.goalText.text = "Get a room";
        playerDialog.SetDialog(" This place is a bloody cesspool ",5f);
    }


    void Update()
    {
       if(bell.GetComponent<bellScript>().isPressed){
           key.GetComponent<Animator>().SetTrigger("DoAnimation");
       }

       if(key.GetComponent<keyScript>().interacted){
           key.SetActive(false);
           playerDialog.SetDialog(" I guess that was efficient at least",5f);
           gameManager.goalText.text = "Go to room number 1";
           room1door.GetComponent<Door>().isLocked = false;
           room1door.GetComponent<Animator>().SetBool("IsUnlocked",true);
           isDone = true;
       }
    }
}
