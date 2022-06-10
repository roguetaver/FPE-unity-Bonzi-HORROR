using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour
{
    public float timeToDestroy;
    void Start()
    {
        StartCoroutine(destroyOnSeconds());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }


    IEnumerator destroyOnSeconds(){
        yield return new WaitForSeconds(timeToDestroy); 
        Destroy(this.gameObject);
    }
}
