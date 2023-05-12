using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material clickedMaterial;
    [SerializeField] protected GameObject gizmoPrefab;
    private Renderer _renderer;
    protected bool _selected;
    protected NavMeshAgent _navMeshAgent;
    protected List<GameObject> drawnGizmos = new List<GameObject>();

    public virtual void Start()
    {
        //Initiate components
        _renderer = GetComponent<Renderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        ChangeMaterial(baseMaterial);
    }

    public virtual void UpdateSelected()
    {
        //Change if the agent is selected
        if(_selected)
        {
            _selected = false;
            RemoveGizmos();
            ChangeMaterial(baseMaterial);
        } 
        else
        {
            _selected = true;
            ShowGizmos();
            ChangeMaterial(clickedMaterial);
        }
    }

    //Function to be implemented when clicked
    public virtual void Click(Vector3 location) { }

    public virtual void FixedUpdate() { }

    //Gizmos are the spheres shown to indicate patrol points
    public virtual void ShowGizmos() { }

    public virtual void RemoveGizmos() { }
    
    private void ChangeMaterial(Material newMaterial)
    {
        _renderer.material = newMaterial;
    }  
}
