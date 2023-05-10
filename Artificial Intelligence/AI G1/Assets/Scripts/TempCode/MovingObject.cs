using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    private Transform currentDestination;

    void Start()
    {
        currentDestination = point1;
    }

    void FixedUpdate()
    {
        //Move object between two positions
        if(transform.position == point1.position)
            currentDestination = point2;
        else if(transform.position == point2.position)
            currentDestination= point1;

        transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, moveSpeed);
    }
}
