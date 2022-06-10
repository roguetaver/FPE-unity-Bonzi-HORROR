using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State3 : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDone;
    private playerTalkingScript playerDialog;
    private GameObject music;

    public bool canSpawnCrabs;
    [SerializeField] GameObject backPack;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject crab;
    [SerializeField] private Transform spawnPoint;
    private bool once1;

    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog").GetComponent<playerTalkingScript>();     
        music = GameObject.Find("Music");
        music.GetComponent<MusicScript>().setAudioClip(2);
        music.GetComponent<MusicScript>().audioSource.Play();
    }


    void Update()
    {
        if(backPack.GetComponent<GenericInteractable>().interacted && !once1){
            gameManager.goalText.text = "Leave";
            music.GetComponent<MusicScript>().setAudioClip(3);
            music.GetComponent<MusicScript>().audioSource.Play();
            backPack.SetActive(false);
            canSpawnCrabs = true;
            Instantiate(crab, spawnPoint.position, Quaternion.identity);
            gameManager.gmAudioSource.Play();
            once1 = true;
        }

        if(canSpawnCrabs && car.GetComponent<carScript>().interacted){
            isDone = true;
        }
    }
}
