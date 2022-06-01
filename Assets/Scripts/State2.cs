using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State2 : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDone;
    private GameObject playerDialog;
    private GameObject music;
    [SerializeField] private GameObject backPackTrans;
    [SerializeField] private GameObject backPackSystem;
    [SerializeField] private GameObject tableDoor;
    [SerializeField] private GameObject fakeDoor;
    [SerializeField] private GameObject lightSwitch;


    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog");    
    }

    void Update()
    {   
        if(lightSwitch.GetComponent<LightSwitchScript>().interacted){
            backPackTrans.SetActive(true);
        }

        if(backPackSystem.GetComponent<backPackSystem>().hasDone){
            fakeDoor.GetComponent<Animator>().SetTrigger("Open");
            tableDoor.GetComponent<Animator>().SetTrigger("Open");
            isDone = true;
        }
    }
}
