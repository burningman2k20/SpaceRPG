using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStats : MonoBehaviour
{
    // Start is called before the first frame update
    HealthStats stats;
    Shields shields;

    void Start()
    {
        stats = GetComponent<HealthStats>();
        shields = stats.gameObject.GetComponent<HealthStats>().shieldObject.GetComponent<Shields>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("saving stats " + gameObject.name);
        // Debug.Log(stats.ToString());
        // Debug.Log(shields.ToString());

    }
}
