using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnObject;
    public bool reSpawn = false;
    void Start()
    {
        if (reSpawn)
        {
            GameObject spawned = Instantiate(spawnObject, transform.position, Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
