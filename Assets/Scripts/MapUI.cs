using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
	public RenderTexture map;
	public Rect windowRect = new Rect(20, 20, 120, 50);
	public bool show = false;

	void OnGUI(){
		if (show) windowRect = GUILayout.Window(100, windowRect, DoMyWindow, "Map");
	}

	void DoMyWindow(int windowID)
	{
	    // This button will size to fit the window
	    GUILayout.Box(map);
	    // if (GUILayout.Button("Hello World"))
	    // {
		//     show = false;
		// //print("Got a click");
	    //
	    // }
	}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Tab)){
		    show = !show;
	    }

    }
}
