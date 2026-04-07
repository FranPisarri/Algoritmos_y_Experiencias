using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LoS : MonoBehaviour
{
    [SerializeField] float range;
    [Range(0, 180)]
    [SerializeField] float angle;

    public bool CheckIfLoS(Vector3 target_pos)
    {
        return CheckInRange(target_pos) && CheckInAngle(target_pos) && !CheckInCover();
    }

    public bool CheckInRange(Vector3 target_pos)
    {
        return (target_pos - transform.position).magnitude <= range;
    }
    public bool CheckInAngle(Vector3 target_pos)
    {
        return Vector3.Angle(target_pos, transform.position) <= angle;
    }
    public bool CheckInCover()
    {
        // TODO: Raycast
        return false;
    }


    // ---------------------------GIZMOS--------------------------- \\
    [SerializeField] bool showGizmos;
    [SerializeField] Color range_color;
    void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.color = range_color;
        Gizmos.DrawSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, CalculatePosition(transform.position, Rotate(transform.up, angle), range));
        Gizmos.DrawLine(transform.position, CalculatePosition(transform.position, Rotate(transform.up, -angle), range));
    }
    Vector2 Rotate(Vector2 v, float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
    Vector2 CalculatePosition(Vector2 init_pos, Vector2 dir, float range)
    {
        return init_pos + dir.normalized * range;
    }
}
