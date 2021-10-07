using UnityEngine;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.SceneManagement;


public enum ObjectiveType
{
	Reach = 0,
	Talk = 1,
	Defeat = 2,
}

public enum ObjectiveStatus
{
	Pending = 0,
	Achieved = 1,
}

public enum ActionOnReach
{
	MarkAsAchieved = 0,
	PlayCinematic = 1,
	PlayAnimation = 2,
	SetTrigger = 3,
}
[System.Serializable]
public class ObjectiveData {
	public string whichScene = "";


    public string Name = "";
    public bool selected;
    [Multiline(10)]
    public string Description;
    public ObjectiveType Kind;
    public ObjectiveStatus Status;
    public GameObject Target;
	public string TargetName;
    public string _Target;
    public bool visible;
    public Objective NextObjective;
	public string NextObjectiveName;
    public ActionOnReach[] ActionsOnReach;
    public Animator animator;
    //public MovieTexture ClipToPlay;
    public string TriggerName;
	public int index;

	public ObjectiveData(){
		//return ObjectiveData;
	}
}

public class Objective : MonoBehaviour
{
	public string whichScene = "";
	public string Name = "";
	public bool selected;
	[Multiline(5)]
	public string Description;
	public ObjectiveType Kind;
	public ObjectiveStatus Status;
	public GameObject Target;
	public string TargetName;
	public string _Target;
	public bool visible;
	public Objective NextObjective;
	public string NextObjectiveName;
	public ActionOnReach[] ActionsOnReach;
	public Animator animator;
	//public MovieTexture ClipToPlay;
	public string TriggerName;
	public int index;

	//public ObjectiveData objective;

	private void OnReach()
    {
        if (ActionsOnReach.Contains(ActionOnReach.MarkAsAchieved)){
            Status = ObjectiveStatus.Achieved;
        }
        if (ActionsOnReach.Contains(ActionOnReach.PlayCinematic))
            this.PlayCinematic();
        if (ActionsOnReach.Contains(ActionOnReach.PlayAnimation))
            this.PlayAnimation();
        if (ActionsOnReach.Contains(ActionOnReach.SetTrigger))
            Debug.Log("trigger something");
        //this.NextObjective.Target.GetComponentInParent<Animator>().SetTrigger(this.TriggerName);

        ParentScript.CurrentObjective = NextObjective;

    }

    private void PlayAnimation()
    {
        Debug.Log("On PlayAnimation: Not implemented yet");
    }

    private void PlayCinematic()
    {
        Debug.Log("On PlayCinematic: Not implemented yet ");
    }

    void OnTriggerEnter(Collider other)
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(other.tag + "  -> " + this.ParentScript.CurrentObjective.name + "  -> " + this.name);
        //Debug.Log(scene.name);
        if (other.tag.Equals("Player") && this.ParentScript.CurrentObjective.name.Equals(this.name) && whichScene.Equals(scene.name))
        {
            OnReach();
        }
    }

    public Objectives ParentScript { get; set; }
	//public
}
