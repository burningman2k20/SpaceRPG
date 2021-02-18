using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlacement : MonoBehaviour
{
    SelectionUI selectUI;
    ObjectiveUI objectiveUI;
    MapUI mapUI;
    InventoryUI inventoryUI;

    [Header("Selection UI")]

    public Rect selectUIRect;
    [Header("Objective UI")]
    public Rect objectiveUIRect;
    [Header("Map UI")]
    public Rect mapUIRect;
    [Header("Inventory UI")]
    public Rect inventoryUIRect;
    [Header("Control Window UI")]
    public Rect controlWindowRect;

    //Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        selectUI = GameObject.Find("UIManager").GetComponent<SelectionUI>();
        //camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //selectUIRect.x = selectUI.target.transform.position.x;
        //selectUIRect.y = selectUI.target.transform.position.y;
        //selectUIRect.x = camera.WorldToViewportPoint(selectUI.target.transform.position.x);
        //selectUIRect.y = camera.WorldToViewportPoint(selectUI.target.transform.position.y);
        selectUI.windowRect = selectUIRect;
        selectUI.controlWindowRect = controlWindowRect;
        selectUI.controlWindowRect.y = Screen.height / 2;


    }

    void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
    }
}
