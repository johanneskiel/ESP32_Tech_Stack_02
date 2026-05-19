using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float maxDistance = 10f;

    private Vector3 startPosition;

    void Start(){
        startPosition = transform.position;
    }

    void Update(){
        if (Vector3.Distance(transform.position, startPosition) > maxDistance){
            Destroy(gameObject);
        }
    }
}