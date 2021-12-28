using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    [SerializeField] int radius;
    [SerializeField] bool showGizmo;
    [SerializeField] Color color;
    void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
