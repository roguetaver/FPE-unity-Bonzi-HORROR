using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    private GameObject PauseMenu;
    private GameObject player;
    void Start()
    {
        PauseMenu = this.transform.GetChild(0).gameObject;
        player = GameObject.Find("Player");
        PauseMenu.SetActive(false);
        gameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        player.GetComponent<FirstPersonController>().canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause(){
        player.GetComponent<FirstPersonController>().canMove = false;
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void menuButton(){
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }
}