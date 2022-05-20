using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip[] adClips;

    public AudioSource audioSource;
    private GameManager gameManager;
    void Start()
    {
        gameManager =  GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = this.GetComponent<AudioSource>();
    }

    public void setAudioClip(int estado){
        audioSource.clip = adClips[estado - 1];
    }
}
