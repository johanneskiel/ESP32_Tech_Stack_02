using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject prefab;

    private GameObject currentObject;

    void Start(){
        SpawnNewObject();
    }

    void Update(){
        if (currentObject == null){
            SpawnNewObject();
        }
    }

    void SpawnNewObject(){
        currentObject = Instantiate(prefab, transform.position, transform.rotation);
    }
}