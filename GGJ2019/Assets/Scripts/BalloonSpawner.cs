using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform start;

    public Transform end;

    public float delayMin = 0.8f;
    public float delayMax = 3f;

    public float speedMin = 3f;
    public float speedMax = 5f;

    public float scaleMin = 0.2f;
    public float scaleMax = 1f;
    void Start()
    {
        Invoke("SpawnBallon", 1f);
    }

    void SpawnBallon()
    {
         var obj = Instantiate(prefab,
            new Vector3(Random.Range(start.position.x, end.position.x), transform.position.y, transform.position.z),
            Quaternion.identity, transform);
        var ballon = obj.GetComponent<Balloon>();
        ballon.speed = Random.Range(speedMin, speedMax);
        ballon.scale = Random.Range(scaleMin, scaleMax);
        Invoke("SpawnBallon", Random.Range(delayMin, delayMax));
    }
}
