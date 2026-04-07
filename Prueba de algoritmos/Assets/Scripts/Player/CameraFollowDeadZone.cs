using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowDeadZone : MonoBehaviour
{ 
    enum FollowType { None, Constant, DeadZone}
    
    [Header("General Parameters")]
    public Transform target;
    private Camera cam;
    [SerializeField] FollowType follow_type;


    [Header("Dead Zone Parameters")]
    [Range(0f, 0.5f)]
    public float deadZoneX = 0.2f;
    [Range(0f, 0.5f)]
    public float deadZoneY = 0.2f;
    public float dz_smoothSpeed = 5f;

    [Header("Constant Follow Parameters")]
    // Límites del mapa
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float smoothSpeed = 5f;



    void Start()
    {
        cam = GetComponent<Camera>();
        switch (follow_type)
        {
            case FollowType.Constant:
                target.GetComponent<PlayerModel>().Moved += ConstantFollow;
                break;
            case FollowType.DeadZone:
                target.GetComponent<PlayerModel>().Moved += FollowDeadZone;
                break;
            case FollowType.None:
                break;
        }
        
    }

    void FollowDeadZone()
    {
        if (target == null) return;

        Vector3 camPos = transform.position;
        Vector3 targetPos = target.position;

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        float deadWidth = horzExtent * deadZoneX;
        float deadHeight = vertExtent * deadZoneY;

        float deltaX = 0f;
        float deltaY = 0f;

        // Horizontal
        if (targetPos.x > camPos.x + deadWidth)
            deltaX = targetPos.x - (camPos.x + deadWidth);
        else if (targetPos.x < camPos.x - deadWidth)
            deltaX = targetPos.x - (camPos.x - deadWidth);

        // Vertical
        if (targetPos.y > camPos.y + deadHeight)
            deltaY = targetPos.y - (camPos.y + deadHeight);
        else if (targetPos.y < camPos.y - deadHeight)
            deltaY = targetPos.y - (camPos.y - deadHeight);

        Vector3 newPos = camPos + new Vector3(deltaX, deltaY, 0);

        // Suavizado
        transform.position = Vector3.Lerp(camPos, newPos, dz_smoothSpeed * Time.deltaTime);
    }

    void ConstantFollow()
    {
        if (target == null) return;

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Posición deseada centrada en el jugador
        float targetX = target.position.x;
        float targetY = target.position.y;

        // Clamp para que no salga del mapa
        float clampedX = Mathf.Clamp(targetX, minBounds.x + horzExtent, maxBounds.x - horzExtent);
        float clampedY = Mathf.Clamp(targetY, minBounds.y + vertExtent, maxBounds.y - vertExtent);

        Vector3 desiredPosition = new Vector3(clampedX, clampedY, transform.position.z);

        // Suavizado
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        switch (follow_type)
        {
            case FollowType.Constant:

                Vector2 top_left = new Vector2(minBounds.x, maxBounds.y);
                Vector2 bottom_right = new Vector2(maxBounds.x, minBounds.y);

                Gizmos.DrawLine(top_left, maxBounds);
                Gizmos.DrawLine(bottom_right, maxBounds);
                Gizmos.DrawLine(top_left, minBounds);
                Gizmos.DrawLine(bottom_right, minBounds);

                break;
            case FollowType.DeadZone:
                break;
            case FollowType.None:
                break;
        }
    }
}