using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;
using UnityEngine.UI;

// Object Selection UI
public class SelectionUI : MonoBehaviour
{
    public List<GameObject> selectObjects;
    public GameObject target;
    public float _distance;
    _Selection selectedObject;
    public Text newUIText;

    public Rect windowRect = new Rect(20, 20, 120, 50);
    public Rect controlWindowRect = new Rect(20, 200, 120, 50);

    void OnGUI()
    {

        // controlWindowRect = GUILayout.Window(11, controlWindowRect, DoMyWindow2, "Controls");
       // if (target != null && selectedObject != null) windowRect = GUILayout.Window(10, windowRect, DoMyWindow, selectedObject.name);

    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        //if (target.GetComponent<Selection>().Equals(null)) return;
        if (target.GetComponent<_Selection>().selectType == selectionType.TakeOff)
        {
            // This button will size to fit the window
            if (GUILayout.Button("Take Off") && _distance < GameObject.FindWithTag("Player").GetComponent<_Selection>().maxDistance)
            {
                //print("take off");
            }
            //print (_distance);
        }
        else
        {
            if (GUILayout.Button("Hello Off"))
            {

            }
        }
    }

    void DoMyWindow2(int windowID)
    {
        // This button will size to fit the window
        GUILayout.Label("Move");
        //GameObject.FindWithTag("Player").GetComponent<ClickToMove>().movable = true;

    }

    public void FindSelectionObjects(string searchTag)
    {
        selectObjects.Clear();
        GameObject[] tempObj = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject sel in tempObj)
        {

            //IndicatorTarget indicator = GameObject.Find(sel.gameObject.name).GetComponent<IndicatorTarget>();
            //IndicatorViewer.UntrackTarget(sel);
            //Debug.Log(sel.gameObject.name  + " " + sel.GetComponent<Target>().indicator.isActive());

             if (target == null ){
                 sel.GetComponent<Target>().indicator.Activate(false);
             } else {
                 target.GetComponent<Target>().indicator.Activate(true);
            }

            //sel.GetComponent<Target>().indicator.Activate(false);
            //indicator.viewer.isTracking = false;
            //if (!sel.GetComponent<T> < seachType > ()) {
            if (!sel.tag.Equals(searchTag))
            {
                selectObjects.Remove(sel);
                //Debug.Log("Removed -> " + sel.name);
            }
            //  if (sel.GetComponent<T> < seachType > ()) {
            if (sel.tag.Equals(searchTag))
            {
                selectObjects.Add(sel);
                //Debug.Log("Kept -> " + sel.name);
            }
        }
    }
    public void FindSelectionObjects()
    {
        selectObjects.Clear();
        GameObject[] tempObj = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject sel in tempObj)
        {
            // if (!target.Equals(null)){
            //     if (target.gameObject != sel.gameObject){
                if (sel.GetComponent<Target>()!=null){
                    if (sel.GetComponent<Target>().indicator != null){
                     sel.GetComponent<Target>().indicator.Activate(false);
                 }
                }
            //}
            //  } else {
            //      target.GetComponent<Target>().indicator.Activate(true);
            // }
            if (!sel.GetComponent<_Selection>())
            {
                selectObjects.Remove(sel);
                //Debug.Log("Removed -> " + sel.name);
            }
            if (sel.GetComponent<_Selection>())
            {
                selectObjects.Add(sel);
                //Debug.Log("Kept -> " + sel.name);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        FindSelectionObjects();
    }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        FindSelectionObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("GameManager").GetComponent<GameManager>().gameStarted) return;
        if (target != null)
        {
            Transform obj1 = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().prefabLocation;
            Transform obj2 = target.transform;

            _distance = Vector3.Distance(obj1.position, obj2.position);
            //Debug.Log(_distance);
        }

        newUIText = GameObject.Find("object").GetComponent<Text>();

        if (target != null) {

            newUIText.text = target.name;

            selectedObject = target.GetComponent<_Selection>();
        } else {
            newUIText.text = "None";
        }
    }
}
