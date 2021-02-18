using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject groundPrefab;

    Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {

        playerPosition = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
