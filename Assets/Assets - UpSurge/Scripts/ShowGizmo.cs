using UnityEngine;

public class ShowGizmo : MonoBehaviour
{
    [SerializeField]
    GizmoType ThisGizmo = GizmoType.Cube;
    [SerializeField]
    float Radius = 1;
    [SerializeField]
    Color GizmoColor = Color.white;

    void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        if(ThisGizmo==GizmoType.Cube) Gizmos.DrawCube(transform.position, new Vector3(Radius, Radius, Radius));
        if(ThisGizmo==GizmoType.Sphere) Gizmos.DrawSphere(transform.position, Radius);
    }

    enum GizmoType
    {
        Cube,
        Sphere
    }
}
