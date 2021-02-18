using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    public int ShieldPoints;

    // Start is called before the first frame update
    void Start()
    {
        Renderer rend;
        rend = gameObject.GetComponent<Renderer>();
        //gameObject.GetComponent<Shields>().ShieldPoints -= 1;
        rend.material.SetFloat("_FresnelPower", gameObject.GetComponent<Shields>().ShieldPoints);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
