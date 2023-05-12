using UnityEngine;

public class RegularAgent : Agent
{
    public override void Click(Vector3 location)
    {
        base.Click(location);

        //Move to click location
        _navMeshAgent.SetDestination(location);
        RemoveGizmos();
        ShowGizmos();
    }

    public override void ShowGizmos()
    {
        base.ShowGizmos();

        var location = base._navMeshAgent.destination;
        var gizmo = Instantiate(base.gizmoPrefab, location, Quaternion.identity);
        base.drawnGizmos.Add(gizmo);
    }

    //Gizmos are the spheres shown to indicate patrol points
    public override void RemoveGizmos()
    {
        base.RemoveGizmos();

        foreach(var gizmo in base.drawnGizmos)
        {
            Destroy(gizmo);
        }
    }
}
