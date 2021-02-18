using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class Window_QuestPointer : MonoBehaviour
{

    [SerializeField] private Camera uiCamera;



    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    SelectionUI selectionUI;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        selectionUI = GameObject.Find("UIManager").GetComponent<SelectionUI>();

        //GameObject.FindWithTag("Target").transform.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }
    private void Awake()
    {
        selectionUI = GameObject.Find("UIManager").GetComponent<SelectionUI>();
        targetPosition = selectionUI.target.transform.position;
        //GameObject.FindWithTag("Target").transform.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }
    private void Update()
    {
        targetPosition = selectionUI.target.transform.position;
        if (targetPosition.Equals(null)) return;
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 40f;

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffscreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        Debug.Log(isOffscreen + " " + targetPositionScreenPoint);

        if (isOffscreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        }
    }
}