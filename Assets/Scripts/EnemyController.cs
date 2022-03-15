using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float relocateTime;
    public bool isRelocated;
    public static bool playerDetected;
    public GameObject player;

    private Rigidbody rb;
    private NavMeshAgent enemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        playerDetected = false;
        isRelocated = true;
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 10f))
        {
            if(hit.transform.tag == "Player")
            {
                playerDetected = true;
            }
        }

        if(playerDetected == true)
        {
            enemy.destination = player.transform.position;
            Debug.Log("PLAYER DETECTED");
        }
        else
        {
            if(isRelocated == true)
            {
                isRelocated = false;
                RandomMovement();
            }
        }
    }

    private void RandomMovement()
    {
        enemy.destination= new Vector3(Random.Range(-14, 14), 0, Random.Range(-14, 14));
        StartCoroutine("Relocation");
    }

    private IEnumerator Relocation()
    {
        yield return new WaitForSeconds(relocateTime);
        isRelocated = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.forward * 10f);
    }
}
