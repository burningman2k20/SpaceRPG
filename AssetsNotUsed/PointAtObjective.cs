using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtObjective : MonoBehaviour
{
    public GameObject uiArrow;
    private Camera _camera;
    public Transform playerPosition;
    public Vector3 targetPosition;
    public bool showArrow = true;

    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        this._camera = Camera.main;
        uiArrow = GameObject.Find("ArrowImage");

    }


    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        //Plane playerPlane = new Plane(Vector3.up, transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }    // Update is called once per frame

    void Update()
    {
        //if (showArrow)
        //{
        playerPosition = GameObject.FindWithTag("Player").transform;//.position;

        //Vector3 targetDirection = playerPosition.position - targetPosition;
        //float speed = 10f;
        //float singleStep = speed * Time.deltaTime;
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        //Debug.DrawRay(transform.position, newDirection, Color.red);
        //transform.rotation = Quaternion.LookRotation(newDirection);
        //Quaternion targetRotation = Quaternion.LookRotation(targetPosition - playerPosition.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        Vector3 heading = playerPosition.InverseTransformPoint(targetPosition);
        angle = Mathf.Atan2(heading.x, heading.y) * Mathf.Rad2Deg - 90;
        uiArrow.transform.eulerAngles = new Vector3(0, 0, -angle);
        //uiArrow.transform.LookAt(targetPosition);
        //}
        //else
        //{
        // uiArrow.SetActive(false);
        //}
    }
}
