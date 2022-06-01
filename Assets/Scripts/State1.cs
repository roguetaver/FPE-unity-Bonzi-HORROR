using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1 : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDone;
    private GameObject playerDialog;
    private GameObject music;
    [SerializeField] private GameObject bell;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject room1door;


    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog");    
    }


    void Update()
    {
       if(bell.GetComponent<bellScript>().isPressed){
           key.GetComponent<Animator>().SetTrigger("DoAnimation");
       }

       if(key.GetComponent<keyScript>().interacted){
           key.SetActive(false);
           room1door.GetComponent<Door>().isLocked = false;
           room1door.GetComponent<Animator>().SetBool("IsUnlocked",true);
           isDone = true;
       }
    }
}
