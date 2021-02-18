using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;

public class Item : MonoBehaviour
{
    public itemType item;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
