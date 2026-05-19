using UnityEngine;
using System.Globalization;

public class TiltListener : MonoBehaviour
{
    public float maxTiltAngle = 15f;
    public float rotationSpeed = 5f;
    
    private float targetRotationX = 0f;
    private float targetRotationZ = 0f;
    private float currentRotationX = 0f;
    private float currentRotationZ = 0f;



    void Update(){
        // Smooth interpolation to target rotation
        currentRotationX = Mathf.Lerp(currentRotationX, targetRotationX, Time.deltaTime * rotationSpeed);
        currentRotationZ = Mathf.Lerp(currentRotationZ, targetRotationZ, Time.deltaTime * rotationSpeed);
        
        // Apply rotation
        transform.rotation = Quaternion.Euler(currentRotationX, 0, currentRotationZ);
    }



    // Called by Ardity SerialController on connection events
    public void OnConnectionEvent(bool success){
        if (success){
            Debug.Log("ESP32 connected");
        }
    }

    // Called by Ardity SerialController when data arrives
    public void OnMessageArrived(string msg){

            // Parse "X:10.5,Y:-5.2" format
            string[] parts = msg.Split(',');
            if (parts.Length != 2) return;
            
            // Extract X value
            string xPart = parts[0].Trim();
            if (!xPart.StartsWith("X:")) return;
            float sensorX = float.Parse(xPart.Substring(2), CultureInfo.InvariantCulture);
            
            // Extract Y value
            string yPart = parts[1].Trim();
            if (!yPart.StartsWith("Y:")) return;
            float sensorY = float.Parse(yPart.Substring(2), CultureInfo.InvariantCulture);
            

            // Map sensor angles to platform rotation
            targetRotationX = Mathf.Clamp(sensorY, -maxTiltAngle, maxTiltAngle);
            targetRotationZ = Mathf.Clamp(-sensorX, -maxTiltAngle, maxTiltAngle);
        
    }

}
