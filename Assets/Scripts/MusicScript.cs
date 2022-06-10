using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip[] adClips;
    public AudioSource audioSource;
    private GameManager gameManager;
    
    void Awake()
    {
        gameManager =  GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = this.GetComponent<AudioSource>();
    }

    public void setAudioClip(int id){
        audioSource.clip = adClips[id];
    }
}
