using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabSpawnSystem : MonoBehaviour
{
    [SerializeField] private Transform triggerPoint;
    [SerializeField] private GameObject crab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        triggerPoint = this.transform.GetChild(0);
        crab = this.transform.GetChild(1).gameObject;
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, triggerPoint.transform.position) < 6f){
            crab.SetActive(true);
            gameManager.gmAudioSource.Play();
        }
    }
}
