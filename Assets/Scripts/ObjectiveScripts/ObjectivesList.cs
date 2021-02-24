using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class mainObjectiveList{
    Objective _objective;
    string _objectiveDataFile;
    bool enabled = false;
    bool active = false;
}

public class ObjectivesList : MonoBehaviour
{
    public GameObject[] objectiveList;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        objectiveList = GameObject.FindGameObjectsWithTag("Objective");
        //if (objectiveList.Length<0) objectiveList = GameObject.FindGameObjectsWithTag("Enemy");

    }
}
