using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private GameObject music;
    public int estadoAtual;
    public Text goalText;


    void Start()
    {
        player = GameObject.Find("Player");
        music = GameObject.Find("Music");
    }

    void Update()
    {
        if(player.GetComponent<FirstPersonController>().isDead){
            Initiate.Fade("restartScene",Color.black, 1f);
        }

        if(estadoAtual == 1){
            this.GetComponent<State1>().enabled = true;
            //setar false os outros estados
            if(this.GetComponent<State1>().isDone){
                estadoAtual += 1;
            }
        }
        
    }
}
