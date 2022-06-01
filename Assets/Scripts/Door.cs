using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool isOpen = false;
    [SerializeField] private bool canBeInteractedWith = true;
    private Animator anim;
    public bool isLocked;

    private void Start(){
        anim = GetComponent<Animator>();
        setDoorState();
    }

    public override void OnFocus(){
        
    }

    public override void OnInteract(){
        if(canBeInteractedWith){
            if(!isLocked){
                isOpen = !isOpen;
                Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
                Vector3 playerTransformDirection = FirstPersonController.instance.transform.position - transform.position;
                float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);

                anim.SetFloat("dot",dot);
                anim.SetBool("IsOpen",isOpen);

                StartCoroutine(AutoClose());
            }
            else{
                anim.SetTrigger("LockAnimation");
            }
        }
    }   

    public override void OnLoseFocus(){
        
    }

    private IEnumerator AutoClose(){
        while(isOpen){
            yield return new WaitForSeconds(3);

            if(Vector3.Distance(transform.position, FirstPersonController.instance.transform.position) > 3){
                isOpen = false;
                anim.SetFloat("dot",0);
                anim.SetBool("IsOpen",isOpen);
            }
        }
    }

    private void Animator_LockInteraction(){
        canBeInteractedWith = false;
    }

    private void Animator_UnlockInteraction(){
        canBeInteractedWith = true;
    }

    private void setDoorState(){
        if(isLocked){
            anim.SetBool("IsUnlocked",false);
        }
        else{
            anim.SetBool("IsUnlocked",true);
        }
    }
}
