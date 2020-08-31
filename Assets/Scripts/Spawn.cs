using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject barril;
    public float tempoSpawn;
    float tempo = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        if (tempo > tempoSpawn)
        {
            tempo = 0;
            Instantiate(barril, transform.position, Quaternion.identity);
        }
    }
}
