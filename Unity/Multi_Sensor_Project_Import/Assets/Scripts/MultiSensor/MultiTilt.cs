using UnityEngine;

public class MultiTilt : MonoBehaviour
{
    public float maxTiltAngle = 15f;
    public float rotationSpeed = 5f;
    public float maxDistance = 10f;
    
    private float targetRotationX = 0f;
    private float targetRotationZ = 0f;
    private float currentRotationX = 0f;
    private float currentRotationZ = 0f;
    private Vector3 startPosition;

    void Start()
    {
        // Save the initial position
        startPosition = transform.position;
    }

    void Update()
    {
        // Check distance from start position
        float distanceFromStart = Vector3.Distance(transform.position, startPosition);
        if (distanceFromStart > maxDistance)
        {
            // Reset to start position
            transform.position = startPosition;
            Debug.Log($"{gameObject.name} exceeded max distance ({distanceFromStart:F2}), reset to start position");
        }
        
        // Smooth interpolation to target rotation
        currentRotationX = Mathf.Lerp(currentRotationX, targetRotationX, Time.deltaTime * rotationSpeed);
        currentRotationZ = Mathf.Lerp(currentRotationZ, targetRotationZ, Time.deltaTime * rotationSpeed);
        
        // Apply rotation
        transform.rotation = Quaternion.Euler(currentRotationX, 0, currentRotationZ);
    }

    // Called by MultiListener to update tilt angles
    public void UpdateTilt(float sensorX, float sensorY)
    {
        // Map sensor angles to platform rotation
        targetRotationX = Mathf.Clamp(sensorY, -maxTiltAngle, maxTiltAngle);
        targetRotationZ = Mathf.Clamp(-sensorX, -maxTiltAngle, maxTiltAngle);
    }

    // Called by MultiListener to set the color
    public void SetColor(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Create a new material instance to avoid modifying the shared material
            renderer.material.color = color;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no Renderer component to set color");
        }
    }
}