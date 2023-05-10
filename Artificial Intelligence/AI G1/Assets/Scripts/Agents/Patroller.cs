using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Patroller : Agent
{
    private List<Vector3> patrolPoints = new List<Vector3>();
    private int currentPatrolPoint = 0;

    //When selected, add the clicked point to patrol points
    public override void Click(Vector3 location)
    {
        base.Click(location);

        location.y += 1;

        if(patrolPoints.Count() < 2)
        {
            patrolPoints.Add(location);
            ShowGizmos();
        }
        else
        {
            patrolPoints.Clear();
            RemoveGizmos();
            patrolPoints.Add(location);
            ShowGizmos();
        }

        if(patrolPoints.Count() == 2)
            ShowGizmos();
    }

    //Keep patrolling between points
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(patrolPoints.Count() == 2)
        {
            if(currentPatrolPoint == patrolPoints.Count())
                currentPatrolPoint = 0;

            if(transform.position == patrolPoints.ElementAt(currentPatrolPoint))
            {
                currentPatrolPoint += 1;
            }
                      
            if(currentPatrolPoint < 2)
            {
                _navMeshAgent.SetDestination(patrolPoints.ElementAt(currentPatrolPoint));
            }
        }
    }

    //Gizmos are the spheres shown to indicate patrol points
    public override void ShowGizmos()
    {
        base.ShowGizmos();

        foreach(var point in patrolPoints)
        {
            var location = point;
            location.y -= 0.95f;
            var gizmo = Instantiate(base.gizmoPrefab, location, Quaternion.identity);
            base.drawnGizmos.Add(gizmo);
        }
    }

    public override void RemoveGizmos()
    {
        base.RemoveGizmos();

        foreach(var gizmo in base.drawnGizmos)
        {
            Destroy(gizmo);
        }
    }
}
