using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    void Start(){
        Cursor.lockState = CursorLockMode.None;
    }

    public void yesButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void noButton()
    {
        Application.Quit(); 
    }
}
