using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
	public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
	cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake(){
	cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	DontDestroyOnLoad(this.gameObject);
    }
}
