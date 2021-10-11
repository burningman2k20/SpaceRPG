using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    public bool showUI = false;
	public ObjectivesList objectives;

    [Header("UI Rect for List")]
    public Rect objectiveListRect;

    //GameObject obj;
    void OnGUI()
    {
        if (showUI)
        {
			//GameObject.Find("Objectives").GetComponent<ObjectivesList>().ReadFile();
			if (GUILayout.Button("Save")){
				objectives.WriteFile("", objectives.playerObjectiveList);
			}
			if (GUILayout.Button("Load")){
				objectives.playerObjectiveList = objectives.ReadFile("");
				foreach(MainObjectiveList obj in objectives.playerObjectiveList){
				GameObject go = GameObject.Find(obj._objectiveObjectName);
				Objective objective = go.GetComponent<Objective>();

					if (obj.completed){
						objective.Status = ObjectiveStatus.Achieved;
					} else {
							//GUILayout.Box(ObjectiveStatus.Pending.ToString());
						objective.Status = ObjectiveStatus.Pending;
					}
				}
				//objectives.WriteFile("", objectives.playerObjectiveList);
			}
			GUILayout.BeginHorizontal();
			GUILayout.Box("Select");
			GUILayout.Box("Obj Name");
			GUILayout.Box("Description");
			GUILayout.Box("Completed?");
			GUILayout.EndHorizontal();
			foreach (MainObjectiveList obj in objectives.playerObjectiveList)
            {
				//Debug.Log(obj.enabled);
				if (!obj.enabled) continue;
				GUILayout.BeginHorizontal();
				if (GameObject.Find(obj._objectiveObjectName)) {

				GameObject go = GameObject.Find(obj._objectiveObjectName);
				Objective objective=go.GetComponent<Objective>();
				if (objective.Status == ObjectiveStatus.Achieved && objective.NextObjective != null) objective.ParentScript.CurrentObjective = objective.NextObjective;


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
				//GUILayout.Box(objective.Name);
				//if (obj.completed){
					//objective.Status = ObjectiveStatus.Achieved;
					GUILayout.Box(objective.Status.ToString());
				//} else {
					//GUILayout.Box(ObjectiveStatus.Pending.ToString());
					//objective.Status = ObjectiveStatus.Pending;
				//}
				} else {
					GUILayout.Box("Select");
					GUILayout.Box(obj._objectiveObjectName);
					GUILayout.Box(" -> this object does not exist in this scene");
					GUILayout.Box(obj.completed.ToString());
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

	void OnSceneLoaded(){
		objectives.playerObjectiveList = objectives.ReadFile("");
		foreach(MainObjectiveList obj in objectives.playerObjectiveList){
		GameObject go = GameObject.Find(obj._objectiveObjectName);
		Objective objective = go.GetComponent<Objective>();

			if (obj.completed){
				objective.Status = ObjectiveStatus.Achieved;
			} else {
					//GUILayout.Box(ObjectiveStatus.Pending.ToString());
				objective.Status = ObjectiveStatus.Pending;
			}
		}
	}

	void Awake()
    {
		objectives = GameObject.Find("Objectives").GetComponent<ObjectivesList>();
        DontDestroyOnLoad(this.gameObject);
    }
}
