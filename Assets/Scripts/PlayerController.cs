using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float playerMove = 10;
    public float playerStop = 0;
    public float stopTime = 1.5f;

    public Camera mainCamera;
    private Vector3 playerInput;
    private Vector3 movePlayer;
    private Vector3 camForward;
    private Vector3 camRight;

    private CharacterController ch;

    private void Awake()
    {
        ch = GetComponent<CharacterController>();
    }

    private void Start()
    {
        playerSpeed = playerMove;
    }
    
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontal, 0, vertical);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        ch.transform.LookAt(ch.transform.position + movePlayer);

        ch.Move(movePlayer * playerSpeed * Time.deltaTime);
    }

    private void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    private void OnTriggerEnter(Collider enemy)
    {
        if(enemy.gameObject.tag == "InteractionZone1")
        {
            playerSpeed = playerStop;
            Debug.Log("PLAYER IS ATTACKED");
            StartCoroutine("PlayerCanMove");
        }
    }

    private void OnTriggerExit(Collider enemy2)
    {
       if(enemy2.gameObject.tag == "InteractionZone2")
       {
           EnemyController.playerDetected = false;
       } 
    }

    private IEnumerator PlayerCanMove()
    {
        yield return new WaitForSeconds(stopTime);
        playerSpeed = playerMove;
    }
}
