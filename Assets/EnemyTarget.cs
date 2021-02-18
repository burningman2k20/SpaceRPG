using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;

public class EnemyTarget : MonoBehaviour
{
    // Start is called before the first frame update
    SelectionUI select;
    public selectionType selectType;
    void OnMouseDown()
    {
        // GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target = gameObject as GameObject;
        //GameObject.Find ("UIManager").GetComponent<SelectionUI> ().FindSelectionObjects ("Enemy");
        Debug.Log(gameObject.name);
    }
    void OnCollisionEnter(Collision collision)
    {
        // foreach (ContactPoint contact in collision.contacts) {
        //     Debug.DrawRay (contact.point, contact.normal, Color.white);
        // }
        // if (collision.relativeVelocity.magnitude > 2)
        //     audioSource.Play ();
        //Destroy (this.gameObject, 10f);
    }
    void Start()
    {
        select = GameObject.Find("UIManager").GetComponent<SelectionUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}