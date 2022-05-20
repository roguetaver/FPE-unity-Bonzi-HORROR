using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    void Start(){
        Cursor.lockState = CursorLockMode.None;
    }

    public void menuButton(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
