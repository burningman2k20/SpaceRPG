using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    public bool showUI = false;
    //GameObject obj;
    void OnGUI()
    {
        if (showUI)
        {
            foreach (GameObject obj in GameObject.Find("Objectives").GetComponent<ObjectivesList>().objectiveList)
            {
                Objective objective=obj.GetComponent<Objective>();
                GUILayout.BeginHorizontal();
                if (!objective.selected && GUILayout.Button("Select")) {
                    objective.selected = true;
                }
                if (objective.selected && GUILayout.Button("Deselect")) {
                    objective.selected = false;
                }
                GUILayout.Box(obj.GetComponent<Objective>().name);
                GUILayout.Box(obj.GetComponent<Objective>().Status.ToString());
                GUILayout.EndHorizontal();
            }
        }

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
