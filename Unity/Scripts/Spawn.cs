using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float time = 1f;

    private float timer = 1000000f;

    void Update(){
        timer += Time.deltaTime;

        if (timer >= time){
            Instantiate(prefab, transform.position, transform.rotation);
            timer = 0f;
        }
    }
}