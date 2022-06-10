using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private GameObject music;
    public int estadoAtual;
    public Text goalText;
    public string location;
    public AudioSource gmAudioSource;
    public bool playerIsDead;


    void Start()
    {
        player = GameObject.Find("Player");
        music = GameObject.Find("Music");
        gmAudioSource = this.GetComponent<AudioSource>();
        estadoAtual = 1;
    }

    void Update()
    {
        if(player.GetComponent<FirstPersonController>().isDead){
            Initiate.Fade("restartScene",Color.black, 1f);
        }

        if(estadoAtual == 1){
            this.GetComponent<State1>().enabled = true;
            this.GetComponent<State2>().enabled = false;
            this.GetComponent<State3>().enabled = false;
            //setar false os outros estados
            if(this.GetComponent<State1>().isDone){
                estadoAtual += 1;
            }
        }
        else if (estadoAtual == 2){
            this.GetComponent<State1>().enabled = false;
            this.GetComponent<State2>().enabled = true;
            this.GetComponent<State3>().enabled = false;
            //setar false os outros estados
            if(this.GetComponent<State2>().isDone){
                estadoAtual += 1;
            }
        }
        else if (estadoAtual == 3){
            this.GetComponent<State1>().enabled = false;
            this.GetComponent<State2>().enabled = false;
            this.GetComponent<State3>().enabled = true;
            //setar false os outros estados
            if(this.GetComponent<State3>().isDone){
                estadoAtual += 1;
            }
        }

        if(playerIsDead){
            BadEnding();
        }

        if(estadoAtual == 4){
            GoodEnding();
        }
        
        
    }

    public void GoodEnding(){
        print("good ending");
    }

    public void BadEnding(){
        print("bad ending");
    }
}
