using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private bool moveAutomatically = true;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAutomatically || Input.GetKeyDown("space"))
        {
            _agent.destination = destination.position;
        }
    }
}