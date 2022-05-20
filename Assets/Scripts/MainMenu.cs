using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene("LevelLoadingScreen");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void quitButton()
    {
        Application.Quit(); 
    }
}
