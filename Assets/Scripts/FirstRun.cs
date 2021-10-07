using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstRun : MonoBehaviour
{
    public string sceneName = "Space";
	public bool showObjectiveUI = false;
	public bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {

        SceneManager.LoadScene(sceneName);
		GameObject.Find("GameManager").GetComponent<GameManager>().gameStarted = gameStarted;
		GameObject.Find("UIManager").GetComponent<ObjectiveUI>().showUI = showObjectiveUI;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
