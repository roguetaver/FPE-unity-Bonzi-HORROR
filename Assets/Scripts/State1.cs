using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1 : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDone;
    private GameObject playerDialog;
    private GameObject music;
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject bell;


    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog");    
    }


    void Update()
    {
       if(bell.GetComponent<bellScript>().isPressed){
           person.GetComponent<Animator>().SetTrigger("DoAnimation");
           isDone = true;
       }
    }
}
