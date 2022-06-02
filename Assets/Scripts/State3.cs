using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State3 : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDone;
    private playerTalkingScript playerDialog;
    private GameObject music;

    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog").GetComponent<playerTalkingScript>();     
    }


    void Update()
    {
        
    }
}
