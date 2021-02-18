using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStats : MonoBehaviour
{
    public int HitPoints;
    //public int ShieldPoints;
    public GameObject shieldObject;
    public bool canDestroy = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // GameObject coliisionObject = collision.collider.gameObject;
        // Debug.Log(coliisionObject.name);
        // Debug.Log(coliisionObject.GetComponent<Shader>().name);

        // foreach (ContactPoint contact in collision.contacts) {
        //     Debug.DrawRay (contact.point, contact.normal, Color.white);
        // }
        // if (collision.relativeVelocity.magnitude > 2)
        //     audioSource.Play ();
        //Destroy (this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}