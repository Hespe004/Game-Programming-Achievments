using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject chooseAgentMenu;

    [Header("What can be clicked?")]
    [SerializeField] private string clickableTag = "Agent";
    
    [Header("Agent type prefabs")]
    [SerializeField] private GameObject agent;
    [SerializeField] private GameObject patroller;

    private Agent selectedAgent;
    private RaycastHit spawnPos;

    //Agent types
    public enum AgentTypes {
        regularAgent,
        patroller
    }
    private AgentTypes selectedAgentType;

    void Update()
    {
        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the ray hits a collider with the desired tag
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(clickableTag))
            {
                // Run OnSelect for clicked object
                OnSelect(hit);
            }
            else {
                GiveInstruction(hit);
            }
        }

        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
        {
            spawnPos = hit;
            // Open choose agent menu
            ChooseAgent();
        }
    }

    void GiveInstruction(RaycastHit hit) {
        // Give agent instructions with NavMesh Hit
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, NavMesh.AllAreas))
        {
            if (selectedAgent != null)
            {
                selectedAgent.Click(navHit.position);
            }
            else
            {
                Debug.Log("No agent selected!");
            }
        }
    }

    //Triggered on left click
    void OnSelect(RaycastHit hit)
    {
        //Get the clicked agent
        Agent clickedAgent = hit.collider.gameObject.GetComponent<Agent>();
        if (selectedAgent != clickedAgent)
        {
            //Update selected agent
            if (selectedAgent != null)
                selectedAgent.UpdateSelected();
            selectedAgent = clickedAgent;
            selectedAgent.UpdateSelected();
        }
        else
        {
            //Remove when clicked on the current agent
            selectedAgent.UpdateSelected();
            selectedAgent = null;
        }
    }

    //Helper method to get the Agent Type based on the selected AgentType enum
    private System.Type GetAgentType(AgentTypes agentType)
    {
        switch(agentType)
        {
            case AgentTypes.regularAgent:
                return typeof(Agent);
            case AgentTypes.patroller:
                return typeof(Patroller);
            default:
                return null;
        }
    }


    //Triggered on right click
    public void OnSpawn(int index)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!(Physics.Raycast(ray, out hit) && hit.collider.CompareTag(clickableTag)))
        {
            //Set the selected agent type
            selectedAgentType = (AgentTypes)index;
        }   

        CloseChooseAgent();
    }


    void ChooseAgent() {
        chooseAgentMenu.SetActive(true);
    }

    void CloseChooseAgent() {
        chooseAgentMenu.SetActive(false);
        
        //Instantiate the selected agent
        if(selectedAgentType==AgentTypes.regularAgent)
            Instantiate(agent, spawnPos.point, Quaternion.identity);
        else
            Instantiate(patroller, spawnPos.point, Quaternion.identity);
    }

}
