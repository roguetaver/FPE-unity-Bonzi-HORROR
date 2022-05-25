using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    [Header("Functional Options")]
    public bool canMove = true;
    public bool canJump = true;
    public bool canSprint = true;
    public bool canCrouch = true;
    public bool canUseHeadbob = true;
    public bool willSlideOnSlopes = true;
    public bool canInteract = true;
    public bool useFootSteps = true;
    public bool hasFlashlight = true;
    public bool fallDamage = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    [SerializeField] private Image crossHair;

    [Header("Movement Parameters")]
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float slopeSpeed = 8f;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);

    [Header("Look Parameters")]
    [SerializeField, Range(1,10)] private float lookSpeedX = 2f;
    [SerializeField, Range(1,10)] private float lookSpeedY = 2f;
    [SerializeField, Range(1,180)] private float upperLookLimit = 80f;
    [SerializeField, Range(1,180)] private float lowerLookLimit = 80f;
    public float mouseSensitivity;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 30f;
    private bool shouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0f,0.5f,0f);
    [SerializeField] private Vector3 standingCenter = new Vector3(0f,0f,0f);
    private bool isCrouching;
    private bool duringCrouchAnimation;
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;

    [Header("Headbob Parameters")]
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;

    //SLIDING PARAMETERS
    private Vector3 hitPointNormal;
    private bool isSliding 
    {
        get
        {
            if(characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = new Vector3 (0.5f, 0.5f, 0f);
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactionLayer;
    private Interactable currentInteractable;

    [Header("Footstep Parameterss")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip[] woodClips;
    [SerializeField] private AudioClip[] metalClips;
    [SerializeField] private AudioClip[] grassClips;
    [SerializeField] private AudioClip[] stoneClips;
    private float footStepTimer = 0f;
    private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : isSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;
    private int layerMaskFootstep;  

    
    //private varivables
    private float defaultYPos = 0;
    private float headBobTimer;
    private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX = 0;
    public static FirstPersonController instance;

    //other
    [SerializeField] private GameObject flashlight;
    [SerializeField] private Vector3 jumpPos;
    [SerializeField] private Vector3 landPos;
    [SerializeField] private bool wasFalling;
    [SerializeField] private bool isFalling;
    public bool isDead;
    [SerializeField] private AudioClip landSound;

    void Awake()
    {
        instance = this;
        
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.visible = false;
        layerMaskFootstep = (-1) - (1 << LayerMask.NameToLayer("Player"));  

    }
    void Update()
    {
        if(hasFlashlight){
            flashlight.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F)){
                flashlight.GetComponent<Flashlight_PRO>().Switch();
            }
        }
        else{
            flashlight.SetActive(false);
        }


        if(canMove)
        {
            HandleMovementInput();
            HandleMouseLook();
            

            if(canJump){
                HandleJump();
            }

            if(canCrouch){
                HandleCrouch();
            }

            if(canUseHeadbob){
                HandleHeadbob();
            }

            if(canInteract){
                HandleInteractInput();
                HandleInteractCheck();
            }

            if(useFootSteps)
            {
                Handle_Footsteps();
            }

            if(fallDamage){
                Handle_Fall_Death();
            }

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2(( isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = moveDirection.y;

        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);

        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0 ,0);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX , 0);

    }

    private void ApplyFinalMovements()
    {
        if(!characterController.isGrounded){
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if(willSlideOnSlopes && isSliding)
        {
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJump()
    {
        if(shouldJump){
            moveDirection.y = jumpForce;
        }
    }

    private void HandleCrouch()
    {
        if(shouldCrouch){
            StartCoroutine(CrouchStand());
        }
    }

    private IEnumerator CrouchStand()
    {
        if(isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f)){
            yield break;
        }

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while(timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed/timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    private void HandleHeadbob()
    {
        if(!characterController.isGrounded)return;

        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            headBobTimer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);

            playerCamera.transform.localPosition = new Vector3(
            playerCamera.transform.localPosition.x , 
            defaultYPos + Mathf.Sin(headBobTimer) * (isCrouching ? crouchBobAmount : isSprinting ? sprintBobAmount : walkBobAmount),
            playerCamera.transform.localPosition.z
            );
        }
    }

    private void HandleInteractCheck(){

        if(currentInteractable != null){
            crossHair.color = Color.red;
        }
        else{
            crossHair.color = Color.white;
        }

        if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance)){
            if(hit.collider.gameObject.layer == 7 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID())){
                hit.collider.TryGetComponent(out currentInteractable);

                if(currentInteractable){
                    currentInteractable.OnFocus();
                }
            }
        }
        else if(currentInteractable){
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractInput(){
        if(Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(
            playerCamera.ViewportPointToRay(interactionRayPoint), 
            out RaycastHit hit , interactionDistance , interactionLayer)){
            currentInteractable.OnInteract();
        }
    }

    private void Handle_Footsteps()
    {
        if(!characterController.isGrounded) return;
        if(currentInput == Vector2.zero) return;

        footStepTimer -= Time.deltaTime;

        if(footStepTimer <= 0)
        {
            if(Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3, layerMaskFootstep))
            {
                switch(hit.collider.tag)
                {
                    case "FootSteps/WOOD":
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0,woodClips.Length - 1)]);
                        break;
                    case "FootSteps/METAL":
                        footstepAudioSource.PlayOneShot(metalClips[Random.Range(0,metalClips.Length - 1)]);
                        break;
                    case "FootSteps/GRASS":
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0,grassClips.Length - 1)]);
                        break;
                    case "FootSteps/STONE":
                        footstepAudioSource.PlayOneShot(stoneClips[Random.Range(0,stoneClips.Length - 1)]);
                        break;
                    default:
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0,woodClips.Length - 1)]);
                        break;
                }
            }

            footStepTimer = GetCurrentOffset;
        }
    }

    private void Handle_Fall_Death()
    {
        if(!characterController.isGrounded && !isFalling){
            wasFalling = false;
            isFalling = true;
            jumpPos = this.transform.position;
        }
        if (isFalling && characterController.isGrounded){
            wasFalling = true;
            isFalling = false;
            landPos = this.transform.position;
        }

        if(jumpPos.y - landPos.y > 1f && wasFalling && !isDead){
            footstepAudioSource.PlayOneShot(landSound);
        }

        if(jumpPos.y - landPos.y > 5f && wasFalling){
            isDead = true;
        }
        else if (!isFalling){
            wasFalling = false;
            landPos = this.transform.position;
            jumpPos = this.transform.position;
        }
    }

    public void AdjustSensibility(float value)
    {
        mouseSensitivity = value;
        PlayerPrefs.SetFloat("sensibility", value);
    }
}

