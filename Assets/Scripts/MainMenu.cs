using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void playButton()
    {
        SceneManager.LoadScene("LevelLoadingScreen");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void quitButton()
    {
        Application.Quit(); 
    }
}
