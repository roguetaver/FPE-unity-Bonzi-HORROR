using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State2 : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDone;
    private playerTalkingScript playerDialog;
    private GameObject music;
    [SerializeField] private GameObject backPackTrans;
    [SerializeField] private GameObject backPackSystem;
    [SerializeField] private GameObject tableDoor;
    [SerializeField] private GameObject fakeDoor;
    [SerializeField] private GameObject lightSwitch;
    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject backPackRegular;
    [SerializeField] private GameObject room1door;
    [SerializeField] private GameObject flashLight;
    private bool done1;
    private bool done2;
    private bool done3;
    private bool done4;
    private bool done5; 
    private bool done6;


    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog").GetComponent<playerTalkingScript>();    
    }

    void Update()
    {   
        if(gameManager.location == "Room1" && !done2){
            playerDialog.SetDialog("It's pretty dark in here ",5f);
            gameManager.goalText.text = "Turn the lights on";
            done2 = true;
        }

        if(lightSwitch.GetComponent<LightSwitchScript>().interacted && !done1){
            playerDialog.SetDialog("  ", 1f);
            gameManager.goalText.text = " Explore the room";
            if(Vector3.Distance(gameManager.player.transform.position, backPackTrans.transform.position) < 3f){
                playerDialog.SetDialog("I should talk to the reception, this bed is disgusting, there is not even a pillow in here ", 15f);
                gameManager.goalText.text = "Leave you backpack at the room";
                backPackTrans.SetActive(true);
                done1 = true;
            }
        }

        if(backPackSystem.GetComponent<backPackSystem>().hasDone && !done6){
            gameManager.goalText.text = "Go to the reception";
            fakeDoor.GetComponent<Animator>().SetTrigger("Open");
            tableDoor.GetComponent<Animator>().SetTrigger("Open");
            done6 = true;
        }

        if(gameManager.location == "Reception" && !done3 && done2){
            playerDialog.SetDialog("Is there someone here? ",5f);
            done3 = true;
        }

        if(letter.GetComponent<LetterScript>().interacted && !done4){
            gameManager.goalText.text = "Grab the flashlight";
            done4 = true;
        }

        if(flashLight.GetComponent<GenericInteractable>().interacted && !done5){
            gameManager.goalText.text = "Read the letter";
            gameManager.player.GetComponent<FirstPersonController>().hasFlashlight = true;
            flashLight.SetActive(false);
            done5 = true;
        }

        if(letter.GetComponent<LetterScript>().interacted && flashLight.GetComponent<GenericInteractable>().interacted){
            backPackRegular.SetActive(false);
            gameManager.goalText.text = "Get your backpack and leave";
            playerDialog.SetDialog(" I need to get my car keys, they are in my backpack ", 10f);
            if(gameManager.location == "Room1"){
                playerDialog.SetDialog(" What happened to my backpack ? ", 12f);
                gameManager.goalText.text = " Leave ";
                room1door.GetComponent<Door>().isLocked = true;
                room1door.GetComponent<Animator>().SetBool("IsUnlocked", false);
                //dialogo quando tentar abrir a porta
                isDone = true;
            }
        }
    }
}
