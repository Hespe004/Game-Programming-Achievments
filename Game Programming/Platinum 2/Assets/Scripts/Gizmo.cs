using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public Color gizmoColor = Color.yellow;
    public float gizmoSize = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
