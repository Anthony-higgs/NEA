using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 5f;      // speed gained per second when moving forward/back
    public float maxSpeed = 5f;          // top speed
    public float turnSpeed = 150f;       // degrees per second
    public float friction = 3f;          // slows the car when no input
    private float currentSpeed = 0f;     // current velocity along forward

    void Update()
    {
        //Input
        float moveInput = Input.GetAxis("Vertical");       // W/S or Up/Down
        float turnInput = Input.GetAxis("Horizontal");     // A/D or Left/Right

        // Rotation
        transform.Rotate(Vector3.forward, -turnInput * turnSpeed * Time.deltaTime);

        //Acceleration/Deceleration
        currentSpeed += moveInput * acceleration * Time.deltaTime;

        //Clamp speed
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        //Friction
        if (Mathf.Abs(moveInput) < 0.01f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, friction * Time.deltaTime);
        }

        //Move Forward
        transform.position += transform.up * currentSpeed * Time.deltaTime;
    }
}