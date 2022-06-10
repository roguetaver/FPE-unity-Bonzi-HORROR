using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSystem : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource audioSource;
    private bool once;

    void Start()
    {
        spawnPoint = this.transform.GetChild(0);
        player = GameObject.Find("Player");
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, spawnPoint.position) < 10f && !once){
            Instantiate(rock, spawnPoint.position, Quaternion.identity);
            Instantiate(rock, spawnPoint.position, Quaternion.identity);
            audioSource.Play();
            once = true;
        }
    }
}
