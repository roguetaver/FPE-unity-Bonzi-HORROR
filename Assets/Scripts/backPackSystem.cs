using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backPackSystem : MonoBehaviour
{
    [SerializeField] private GameObject backPackTrans;
    [SerializeField] private GameObject backPackRegular;
    public bool hasDone;

    void Start()
    {

    }

    void Update()
    {
        if(backPackTrans.GetComponent<GenericInteractable>().interacted && !hasDone){
            backPackTrans.SetActive(false);
            backPackRegular.SetActive(true);
            hasDone = true;
        }
    }
}
