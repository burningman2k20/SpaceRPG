using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{

    [Header("Objectives")]
    [SerializeField] public Objective CurrentObjective;
    private Objective[] PlayerObjectives;
    [SerializeField] public Image CurrentObjectiveArrow;

    [SerializeField] public Text CurrentObjectiveDescription;

 	public string jsonFileName = "default.json";
 	public List<Objective> objectiveList = new List<Objective>();

	public static ObjectiveData CreateFromJSON(string jsonString) {
	        return JsonUtility.FromJson<ObjectiveData>(jsonString);
	}

	public string CreateJSON(ObjectiveData objectiveData) {
        return JsonUtility.ToJson(objectiveData);
    }

    void Start()
    {
        var objectiveParentGameObject = this.CurrentObjective.transform.parent.gameObject;
        if (objectiveParentGameObject != null)
        {
            this.PlayerObjectives = objectiveParentGameObject.GetComponentsInChildren<Objective>();
            if (this.PlayerObjectives != null)
            {
                //Debug.Log("Successfully found all player objectives");
                foreach (Objective singleObjective in PlayerObjectives)
                {
                    if (singleObjective != null)
                    {
                        singleObjective.ParentScript = this;
                    }
                }
            }
            //else
            //Debug.LogError("Unable to find objectives");
        }
    }

    void OnGUI()
    {
        if (this.CurrentObjective != null)
        {
            this.CurrentObjectiveDescription.text = this.CurrentObjective.Description;
            //GameObject.Find("ArrowImage").GetComponent<PointAtObjective>().showArrow = true;
            //GameObject.Find("ArrowImage").GetComponent<PointAtObjective>().targetPosition = this.CurrentObjective.gameObject.transform.position;
        }
        else
        {
            //GameObject.Find("ArrowImage").GetComponent<PointAtObjective>().showArrow = false;
            //GameObject.Find("ArrowImage").GetComponent<PointAtObjective>().targetPosition = ;
            this.CurrentObjectiveDescription.text = "";
        }

        GUILayout.BeginArea(new Rect(0,0,250,250));
        GUILayout.Label( this.CurrentObjectiveDescription.text);
        GUILayout.EndArea();
    }
}
