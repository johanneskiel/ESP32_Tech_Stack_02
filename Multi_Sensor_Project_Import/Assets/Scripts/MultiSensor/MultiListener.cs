using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class MultiListener : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject tiltPrefab;
    
    [Header("Timeout Settings")]
    public float timeout = 3f;
    
    private Dictionary<string, TrackedObject> trackedObjects = new Dictionary<string, TrackedObject>();
    
    // Helper class to track objects and their last update time
    private class TrackedObject
    {
        public GameObject gameObject;
        public MultiTilt tiltScript;
        public float lastUpdateTime;
        
        public TrackedObject(GameObject obj, MultiTilt script)
        {
            gameObject = obj;
            tiltScript = script;
            lastUpdateTime = Time.time;
        }
    }

    void Update()
    {
        // Check for timeouts and remove stale objects
        List<string> toRemove = new List<string>();
        
        foreach (var kvp in trackedObjects)
        {
            if (Time.time - kvp.Value.lastUpdateTime > timeout)
            {
                toRemove.Add(kvp.Key);
            }
        }
        
        // Remove timed-out objects
        foreach (string name in toRemove)
        {
            Debug.Log($"Timeout: Removing {name}");
            Destroy(trackedObjects[name].gameObject);
            trackedObjects.Remove(name);
        }
    }

    // Called by Ardity SerialController on connection events
    public void OnConnectionEvent(bool success)
    {
        if (success)
        {
            Debug.Log("ESP32 connected - MultiListener ready");
        }
        else
        {
            Debug.Log("ESP32 disconnected");
        }
    }

    // Called by Ardity SerialController when data arrives
    // Expected format: "name,color,x,y"
    // Example: "simple44,#FFFFFF,9.00,5.50"
    public void OnMessageArrived(string msg)
    {
        try
        {
            // Parse "name,color,x,y" format
            string[] parts = msg.Split(',');
            if (parts.Length != 4)
            {
                Debug.LogWarning($"Invalid message format: {msg}");
                return;
            }
            
            // Extract values
            string name = parts[0].Trim();
            string colorHex = parts[1].Trim();
            float sensorX = float.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);
            float sensorY = float.Parse(parts[3].Trim(), CultureInfo.InvariantCulture);
            
            // Parse color
            Color color = ParseHexColor(colorHex);
            
            // Check if this object already exists
            if (trackedObjects.ContainsKey(name))
            {
                // Update existing object
                TrackedObject tracked = trackedObjects[name];
                tracked.lastUpdateTime = Time.time;
                tracked.tiltScript.UpdateTilt(sensorX, sensorY);
            }
            else
            {
                // Create new object
                CreateNewTrackedObject(name, color, sensorX, sensorY);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error parsing message '{msg}': {e.Message}");
        }
    }

    private void CreateNewTrackedObject(string name, Color color, float sensorX, float sensorY)
    {
        if (tiltPrefab == null)
        {
            Debug.LogError("Tilt Prefab is not assigned!");
            return;
        }
        
        // Instantiate prefab
        GameObject newObj = Instantiate(tiltPrefab, transform.position, Quaternion.identity);
        newObj.name = name;
        
        // Get MultiTilt component
        MultiTilt tiltScript = newObj.GetComponent<MultiTilt>();
        if (tiltScript == null)
        {
            Debug.LogError($"Prefab {tiltPrefab.name} does not have a MultiTilt component!");
            Destroy(newObj);
            return;
        }
        
        // Set color
        tiltScript.SetColor(color);
        
        // Initialize tilt values
        tiltScript.UpdateTilt(sensorX, sensorY);
        
        // Track the object
        trackedObjects[name] = new TrackedObject(newObj, tiltScript);
        
        Debug.Log($"Created new tracked object: {name} with color {color} at X:{sensorX}, Y:{sensorY}");
    }

    // Parse hex color string (e.g., "#FFFFFF" or "FFFFFF") to Unity Color
    private Color ParseHexColor(string hexColor)
    {
        // Remove # if present
        if (hexColor.StartsWith("#"))
        {
            hexColor = hexColor.Substring(1);
        }
        
        // Default to white if parsing fails
        Color color = Color.white;
        
        if (ColorUtility.TryParseHtmlString("#" + hexColor, out color))
        {
            return color;
        }
        else
        {
            Debug.LogWarning($"Failed to parse color: {hexColor}, using white instead");
            return Color.white;
        }
    }
    
    // Optional: Clean up all tracked objects when this component is destroyed
    void OnDestroy()
    {
        foreach (var kvp in trackedObjects)
        {
            if (kvp.Value.gameObject != null)
            {
                Destroy(kvp.Value.gameObject);
            }
        }
        trackedObjects.Clear();
    }
}