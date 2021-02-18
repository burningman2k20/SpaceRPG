using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;

public class Selection : MonoBehaviour
{
    //bool clear = false;
    public float maxDistance = 1f;
    SelectionUI select;

    public selectionType selectType;
    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        //Destroy(gameObject);
        
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
        select = GameObject.Find("UIManager").GetComponentInChildren<SelectionUI>();
        select.target = gameObject as GameObject;
        //select.FindSelectionObjects();
        if (gameObject.GetComponent<Target>() != null) {
            
            if (gameObject.GetComponent<Target>().indicator != null) gameObject.GetComponent<Target>().indicator.Activate(true);
            //Debug.Log(gameObject.GetComponent<Target>().indicator.isActive());
        }

        //IndicatorTarget indicator = ;
        //IndicatorViewer.TrackTarget(gameObject);
        // if (GetComponent<LandingArea>() != null)
        // {
        //     // print("landing area " + gameObject.name);
        // }
        //clear = false;

    }

    // void OnMouseExit()
    // {
    //     //clear = true;
    // }

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
        select = GameObject.Find("UIManager").GetComponent<SelectionUI>();
    }

    void FixedUpdate()
    {
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
        select = GameObject.Find("UIManager").GetComponent<SelectionUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
        //select = GameObject.Find("Manager").GetComponent<SelectionUI>();
        if (select != null) {
            if (select.target != null)
            {
                if (select._distance < maxDistance)
                {
                    //print("Close to object" + select.target.name);
                }
            }
        }
        //if (clear)
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().target =null;
    }
}