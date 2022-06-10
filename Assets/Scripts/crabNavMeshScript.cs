using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crabNavMeshScript : MonoBehaviour
{
    [SerializeField] private Transform targetPostion;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float soundTimer;
    [SerializeField] private float startSoundTimer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] monsterAudioClips;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;

    private void Awake(){
        targetPostion = GameObject.Find("Player").transform;
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
        animator.SetTrigger("Walk_Cycle_1");
        startSoundTimer = 2f;
        soundTimer = startSoundTimer;
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        agent.destination = targetPostion.position;
        playRandomAudio();

        if(Vector3.Distance(player.transform.position,this.transform.position) < 3f){
            gameManager.playerIsDead = true;
        }
    }

    private void playRandomAudio()
    {
        if(!audioSource.isPlaying){
            audioSource.clip = monsterAudioClips[Random.Range(0,monsterAudioClips.Length - 1)];

            audioSource.Play();
        }
    }

}
