using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed = 5f;

    void Update(){
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}