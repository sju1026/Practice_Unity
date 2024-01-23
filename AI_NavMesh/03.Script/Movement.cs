using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goalPosition)
    {
        StopCoroutine("OnMove");
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.SetDestination(goalPosition);
        StartCoroutine("OnMove");
    }

    IEnumerator OnMove()
    {
        while (true)
        {
            if (Vector3.Distance(navMeshAgent.destination, transform.position) < 0.1f)
            {
                transform.position = navMeshAgent.destination;
                navMeshAgent.ResetPath();

                break;
            }
            yield return null;
        }
    }
}
