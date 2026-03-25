using UnityEngine;

public class PlanetVerlet : MonoBehaviour
{
    public Transform sun;

    public float G = 39.478f;
    public float dt = 0.01f;
    public float velocity;

    private Vector3 currentPosition;
    private Vector3 previousPosition;

    void Start()
    {
        currentPosition = transform.position;

        Vector3 initialVelocity = new Vector3(0, 0, velocity);

        previousPosition = currentPosition - initialVelocity * dt;
    }

    void FixedUpdate()
    {
        Vector3 direction = sun.position - currentPosition;
        float distance = direction.magnitude;

        Vector3 acceleration = direction.normalized * (G / (distance * distance));

        Vector3 newPosition = 2 * currentPosition - previousPosition + acceleration * dt * dt;

        previousPosition = currentPosition;
        currentPosition = newPosition;

        transform.position = currentPosition;
    }
}