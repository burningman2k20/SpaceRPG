using PixelPlay.OffScreenIndicator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attach the script to the off screen indicator panel.
/// </summary>
[DefaultExecutionOrder(-1)]
public class OffScreenIndicator : MonoBehaviour
{
    [Range(0.5f, 0.9f)]
    [Tooltip("Distance offset of the indicators from the centre of the screen")]
    [SerializeField] private float screenBoundOffset = 0.9f;

    public Camera mainCamera;
    public Camera mapCamera;
    public Vector3 screenCentre;
    private Vector3 screenBounds;

public Color clr = Color.red;
    public List<Target> targets = new List<Target>();

    public static Action<Target, bool> TargetStateChanged;

    void Awake()
    {
        mainCamera = Camera.main;
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        screenBounds = screenCentre * screenBoundOffset;
        TargetStateChanged += HandleTargetStateChanged;
    }

void Start(){
    foreach(Target target in targets)
        {
            target.indicator.Activate(false);
        }
}
void Update(){
//    DrawIndicators(mainCamera);
}
    void LateUpdate()
    {
        //DrawIndicators(mapCamera);
        DrawIndicators(mainCamera);
        foreach(Target target in targets)
        {
         //   Debug.Log(target.gameObject.name + " -> " + target.indicator.isActive());
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="GameObject"></param>
    public bool getObjective(GameObject obj){
          //foreach(Objective _obj in targets){
		  //GameObject temp = GameObject.GetComponent<Objective>().objective.targetName);
          //}
		  //if (temp != null)
          return obj.GetComponent<Objective>().selected;
		  //else return false;
        //return false;
    }

    /// <summary>
    /// Draw the indicators on the screen and set thier position and rotation and other properties.
    /// </summary>
    void DrawIndicators(Camera camera)
    {
        foreach(Target target in targets)
        {
            if (target.indicator != null){
				Debug.Log(target.gameObject);
                bool GOenabled = getObjective(target.gameObject);
                //Debug.Log(enabled);
                if (GOenabled) {
                    target.indicator.Activate(true);
                } else {
                    target.indicator.Activate(false);
                }
                if (!target.indicator.isActive()) continue;
            } else {
                //Debug.Log("Nothing");
            }




            //Debug.Log(target.isCompleted);
            //   SelectionUI select = GameObject.Find("UIManager").GetComponent<SelectionUI>();

        //    if (target.indicator != null && select.target != null) {
        //        target.indicator.Activate(false);
        //    } else
            clr = target.TargetColor;
        //       if (select.target == null){
        //           target.indicator.Activate(false);
        //       } else { // if (target.gameObject.Equals(select.target.gameObject  )) {
        // // //         //  clr = Color.blue;
        //            target.indicator.Activate(true);
        //         }// else {
        //          // target.indicator.Activate(false);
        //           clr = target.TargetColor;
        //       }
        //        if (select.target == null) {
        //             clr = target.TargetColor;
        //     //        //target.indicator.Activate(false);
        //         }


            Vector3 screenPosition = OffScreenIndicatorCore.GetScreenPosition(camera, target.transform.position);
            bool isTargetVisible = OffScreenIndicatorCore.IsTargetVisible(screenPosition);
            float distanceFromCamera = target.NeedDistanceText ? target.GetDistanceFromCamera(camera.transform.position) : float.MinValue;// Gets the target distance from the camera.
            Indicator indicator = null;

            if(target.NeedBoxIndicator && isTargetVisible)
            {
                screenPosition.z = 0;
                indicator = GetIndicator(ref target.indicator, IndicatorType.BOX); // Gets the box indicator from the pool.
            }
            else if(target.NeedArrowIndicator && !isTargetVisible)
            {
                float angle = float.MinValue;
                OffScreenIndicatorCore.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                indicator = GetIndicator(ref target.indicator, IndicatorType.ARROW); // Gets the arrow indicator from the pool.
                indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.


            }
            else
            {
                target.indicator?.Activate(false);
                target.indicator = null;
            }
            if(indicator)
            {
                indicator.SetImageColor(clr);// Sets the image color of the indicator.
                indicator.SetDistanceText(distanceFromCamera); //Set the distance text for the indicator.
                indicator.transform.position = screenPosition; //Sets the position of the indicator on the screen.
                indicator.SetTextRotation(Quaternion.identity); // Sets the rotation of the distance text of the indicator.
            }
        //  }else {
        //        target.indicator?.Activate(false);
        //         target.indicator = null;
        // }

    }
    }

    /// <summary>
    /// 1. Add the target to targets list if <paramref name="active"/> is true.
    /// 2. If <paramref name="active"/> is false deactivate the targets indicator,
    ///     set its reference null and remove it from the targets list.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="active"></param>
    private void HandleTargetStateChanged(Target target, bool active)
    {
        if(active)
        {
            targets.Add(target);
        }
        else
        {
            target.indicator?.Activate(false);
            target.indicator = null;
            targets.Remove(target);
        }
    }

    /// <summary>
    /// Get the indicator for the target.
    /// 1. If its not null and of the same required <paramref name="type"/>
    ///     then return the same indicator;
    /// 2. If its not null but is of different type from <paramref name="type"/>
    ///     then deactivate the old reference so that it returns to the pool
    ///     and request one of another type from pool.
    /// 3. If its null then request one from the pool of <paramref name="type"/>.
    /// </summary>
    /// <param name="indicator"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private Indicator GetIndicator(ref Indicator indicator, IndicatorType type)
    {
        if(indicator != null)
        {
            if(indicator.Type != type)
            {
                indicator.Activate(false);
                indicator = type == IndicatorType.BOX ? BoxObjectPool.current.GetPooledObject() : ArrowObjectPool.current.GetPooledObject();
                indicator.Activate(true); // Sets the indicator as active.

            }
        }
        else
        {
            indicator = type == IndicatorType.BOX ? BoxObjectPool.current.GetPooledObject() : ArrowObjectPool.current.GetPooledObject();
            indicator.Activate(true); // Sets the indicator as active.
        }
        return indicator;
    }

    private void OnDestroy()
    {
        TargetStateChanged -= HandleTargetStateChanged;
    }
}
