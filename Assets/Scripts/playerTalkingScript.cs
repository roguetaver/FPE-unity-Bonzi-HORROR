using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTalkingScript : MonoBehaviour
{
    private bool textSetted;
    private float textTime;
    void Start()
    {
        
    }

    void Update()
    {
        if(textSetted){
            textTime -= Time.deltaTime;
            if(textTime <= 0){
                this.GetComponent<Text>().text = " ";
                textSetted = false;
            }
        }
        
    }

    public void SetDialog(string dialog , float time){
        this.GetComponent<Text>().text = dialog;
        textTime = time;
        textSetted = true;
    }
}
