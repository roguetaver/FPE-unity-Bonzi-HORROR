using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCrabScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetTrigger("Eat_Cycle_1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
