using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    public bool showUI = false;

    [Header("UI Rect for List")]
    public Rect objectiveListRect;

    //GameObject obj;
    void OnGUI()
    {
        if (showUI)
        {
			//GameObject.Find("Objectives").GetComponent<ObjectivesList>().ReadFile();
			if (GUILayout.Button("Load")){
				GameObject.Find("Objectives").GetComponent<ObjectivesList>().ReadFile();
			}
			GUILayout.BeginHorizontal();
			GUILayout.Box("Select");
			GUILayout.Box("Obj Name");
			GUILayout.Box("Description");
			GUILayout.Box("Pending?");
			GUILayout.EndHorizontal();
			foreach (MainObjectiveList obj in GameObject.Find("Objectives").GetComponent<ObjectivesList>().masterObjectiveList)
            {
				GUILayout.BeginHorizontal();
				if (GameObject.Find(obj._objectiveObjectName)) {

				GameObject go = GameObject.Find(obj._objectiveObjectName);
				Objective objective=go.GetComponent<Objective>();

				if (!go.GetComponent<Target>().enabled && GUILayout.Button("Select")) {
					go.GetComponent<Target>().enabled = true;
                    objective.selected = true;
                }
				if (go.GetComponent<Target>().enabled && GUILayout.Button("Deselect")) {
					go.GetComponent<Target>().enabled = false;
                    objective.selected = false;
                }

				GUILayout.Box(obj._objectiveObjectName);
				GUILayout.Box(" -> this object exists in this scene");
				GUILayout.Box(objective.Name);
				GUILayout.Box(objective.Status.ToString());
				} else {
					GUILayout.Box("Select");
					GUILayout.Box(obj._objectiveObjectName);
					GUILayout.Box(" -> this object does not exist in this scene");
				//	GUILayout.EndHorizontal();
				}

				// GUILayout.BeginHorizontal();
                // if (!objective.selected && GUILayout.Button("Select")) {
				// 	obj.GetComponent<Target>().enabled = true;
                //     objective.selected = true;
                // }
                // if (objective.selected && GUILayout.Button("Deselect")) {
				// 	obj.GetComponent<Target>().enabled = false;
                //     objective.selected = false;
                // }

                 GUILayout.EndHorizontal();
			}

            // foreach (GameObject obj in GameObject.Find("Objectives").GetComponent<ObjectivesList>().objectiveList)
            // {
            //     Objective objective=obj.GetComponent<Objective>();
			// 	//ObjectiveData objData = new ObjectiveData();
			// 	//objData.Name = objective.Name;
			// 	//Debug.Log(objData.Name);
            //     GUILayout.BeginHorizontal();
            //     if (!objective.selected && GUILayout.Button("Select")) {
			// 		obj.GetComponent<Target>().enabled = true;
            //         objective.selected = true;
            //     }
            //     if (objective.selected && GUILayout.Button("Deselect")) {
			// 		obj.GetComponent<Target>().enabled = false;
            //         objective.selected = false;
            //     }
            //     GUILayout.Box(objective.Name);
            //     GUILayout.Box(objective.Status.ToString());
            //     GUILayout.EndHorizontal();
            // }
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
