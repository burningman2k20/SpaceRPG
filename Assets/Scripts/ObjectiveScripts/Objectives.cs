using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{

    [Header("Objectives")]
    [SerializeField] public Objective CurrentObjective;
    private Objective[] PlayerObjectives;
    [SerializeField] public Image CurrentObjectiveArrow;

    [SerializeField] public Text CurrentObjectiveDescription;

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
    }
}