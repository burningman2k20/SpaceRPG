using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 10;
    public int damage = 1;
    public GameObject bulletPrefab;
    public GameObject explosion;
    void Start()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        // foreach (ContactPoint contact in collision.contacts) {
        //     Debug.DrawRay (contact.point, contact.normal, Color.white);
        // }
        // if (collision.relativeVelocity.magnitude > 2)
        //     audioSource.Play ();
        GameObject coliisionObject = collision.collider.gameObject;
        Renderer rend;
        rend = coliisionObject.GetComponent<Renderer>();

        //rend.material.shader = Shader.Find("Specular");
        //Debug.Log(coliisionObject.name);
        //Debug.Log(coliisionObject.GetComponent<Renderer>().sharedMaterial);
        if (coliisionObject.GetComponent<HealthStats>() != null)
        {
            coliisionObject.GetComponent<HealthStats>().HitPoints -= 1;
            //rend.material.SetFloat("_FresnelPower", coliisionObject.GetComponent<HealthStats>().HitPoints);
            if (coliisionObject.GetComponent<HealthStats>().HitPoints < 1)
            {
                if (coliisionObject.GetComponent<HealthStats>().canDestroy) Destroy(coliisionObject);
                Debug.Log("no hitpoints left");
            }
        }
        if (coliisionObject.GetComponent<Shields>() != null)
        {
            coliisionObject.GetComponent<Shields>().ShieldPoints -= 1;
            rend.material.SetFloat("_FresnelPower", coliisionObject.GetComponent<Shields>().ShieldPoints);
            if (coliisionObject.GetComponent<Shields>().ShieldPoints < 1) Destroy(coliisionObject);
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}