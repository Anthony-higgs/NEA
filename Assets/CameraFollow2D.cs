using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;       // assigned to my player
    public float smoothSpeed = 5f; // how fast the camera follows
    public Vector3 offset;         // optional offset so camera isn't exactly at player center

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position = player's position + offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move camera towards desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Apply smoothed position (keep camera's original Z)
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}