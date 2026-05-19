using UnityEngine;
using System.Globalization;

public class WaveListener : MonoBehaviour{
    public float distance = 5f;  // Maximale Y-Bewegung
    
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Called by Ardity SerialController on connection events
    public void OnConnectionEvent(bool success)
    {
        if (success)
        {
            Debug.Log("ESP32 Wassersensor connected");
        }
    }

    // Called by Ardity SerialController when data arrives
    public void OnMessageArrived(string msg)
    {
        // Parse value (0-1)
        float value = float.Parse(msg.Trim(), CultureInfo.InvariantCulture);
        
        // Calculate Y position: 0 to distance
        float newPosition = startPosition.y + value * distance;
        
        // Set position 
        transform.position = new Vector3(startPosition.x,  newPosition,  startPosition.z);
    }
}