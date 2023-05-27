using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    private CustomBehaviorTree behaviorTree;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        behaviorTree = new CustomBehaviorTree();
        behaviorTree.AddNode(new FollowPlayerNode(agent, GameObject.FindGameObjectWithTag("Player").transform));
        behaviorTree.AddNode(new PatrolNode(agent, waypoints));
        behaviorTree.AddNode(new SearchNode(agent));
    }

    private void Update()
    {
        behaviorTree.Execute();
    }
}

public abstract class BehaviorNode
{
    public abstract void Execute();
}

public class CustomBehaviorTree
{
    private List<BehaviorNode> nodes = new List<BehaviorNode>();

    public void AddNode(BehaviorNode node)
    {
        nodes.Add(node);
    }

    public void Execute()
    {
        foreach (BehaviorNode node in nodes)
        {
            node.Execute();
        }
    }
}

public class FollowPlayerNode : BehaviorNode
{
    private NavMeshAgent agent;
    private Transform player;

    public FollowPlayerNode(NavMeshAgent agent, Transform player)
    {
        this.agent = agent;
        this.player = player;
    }

    public override void Execute()
    {
        // Calculate the distance between the agent and the player
        float distance = Vector3.Distance(agent.transform.position, player.position);

        if (distance<60) {
            agent.SetDestination(player.position);
        }
    }

}

public class PatrolNode : BehaviorNode
{
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private int currentWaypoint = 0;

    public PatrolNode(NavMeshAgent agent, Transform[] waypoints)
    {
        this.agent = agent;
        this.waypoints = waypoints;
    }

    public override void Execute()
    {
        // Implement the logic to patrol between waypoints
        // Set the agent's destination to the current waypoint's position
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}

public class SearchNode : BehaviorNode
{
    private NavMeshAgent agent;
    private float nextSearchTime;
    private FieldOfView fov;
    private Timer resetTimer;
    private float resetDelay = 2f; // Adjust the delay as desired
    private float originalFov;

    public SearchNode(NavMeshAgent agent)
    {
        this.agent = agent;
        // Set the initial time for the first search
        nextSearchTime = Time.time + Random.Range(10f, 20f);
        fov = agent.GetComponent<FieldOfView>();
    }

    public override void Execute()
    {
        if (Time.time >= nextSearchTime)
        {
            originalFov = fov.viewRadius;

            // Start the search coroutine
            PerformSearch();

            // Reset search radius after a delay
            StartResetTimer();

            // Set the next search time for the next random interval
            nextSearchTime = Time.time + Random.Range(5f, 10f);
        }
    }

    private void PerformSearch()
    {
        fov.viewRadius *= 1.5f;
    }

    private void StartResetTimer()
    {
        if (resetTimer != null)
        {
            resetTimer.Stop();
            resetTimer.Dispose();
        }

        resetTimer = new Timer(resetDelay * 1000); // Convert delay to milliseconds
        resetTimer.Elapsed += ResetViewRadius;
        resetTimer.AutoReset = false;
        resetTimer.Start();
    }

    private void ResetViewRadius(object sender, ElapsedEventArgs e)
    {
        fov.viewRadius = originalFov; // Assuming originalFov is accessible here
    }
}