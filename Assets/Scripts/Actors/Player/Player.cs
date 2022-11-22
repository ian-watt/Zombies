using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]

public class Player : Actor
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool isGroundedT;
    public bool canShoot;
    public LayerMask shootMask;
    UnityEvent<Interactable> interactEvent = new UnityEvent<Interactable>();

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    void Start()
    {
        GameManager.Instance.CurrentPlayers.Add(this);

        characterController = GetComponent<CharacterController>();

        canShoot = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interactEvent.AddListener(OnInteract);

    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit info;
            Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out info, Mathf.Infinity, shootMask);
            {
                Debug.DrawLine(transform.position, info.point);
                Debug.Log(info.transform.name);
                if (info.transform.gameObject.tag == "Interactable")
                {
                    {
                        Interactable arg0 = info.transform.gameObject.GetComponent<Interactable>();
                        interactEvent.Invoke(arg0);
                    }
                }

            }
        }
    }

    private void HandleShooting()
    {
        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit info;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out info, Mathf.Infinity, shootMask))
                {
                    Debug.Log("You hit " + info.transform.name);
                    if(info.transform.parent.tag == "Enemy")
                    {
                        info.transform.parent.GetComponent<Enemy>().health -= 1;
                    }
                }
            }
        }
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float currentSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);


        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void OnInteract(Interactable interact)
    {
        Debug.Log("Interacted with " + interact.transform.name);
    }
}