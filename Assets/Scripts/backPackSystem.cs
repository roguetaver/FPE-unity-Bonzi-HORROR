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
        backPackRegular = this.transform.GetChild(0).gameObject;
        backPackTrans = this.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(backPackTrans.GetComponent<GenericInteractable>().interacted){
            backPackTrans.SetActive(false);
            backPackRegular.SetActive(true);
            hasDone = true;
        }
    }
}
