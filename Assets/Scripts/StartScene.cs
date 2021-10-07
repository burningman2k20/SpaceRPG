using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameDataTypes;

public class StartScene : MonoBehaviour
{
    public enum location_t
    {
        Ground, Air, Space, Building
    }

    public location_t Location;

	public bool showObjectiveUI = false;
	public bool gameStarted = false;

    GameManager gameManager;
	ObjectivesList objectiveList;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		objectiveList = GameObject.Find("Objectives").GetComponent<ObjectivesList>();
		gameManager.gameStarted = gameStarted;
		GameObject.Find("UIManager").GetComponent<ObjectiveUI>().showUI = showObjectiveUI;
		//objectiveList.objectiveList.RemoveAll()
		//objectiveList = GameObject.Find("Objectives").GetComponent<ObjectivesList>();
		//objectiveList.objectiveList = GameObject.FindGameObjectsWithTag("Objective");
		//objectiveList.loadObjectives = true;
//		objectiveList.WriteFile();
		objectiveList.ReadFile();
        gameManager.enabled = true;
        GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
        gameManager.SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        if (gameManager.playerLocation != locationType.Building)
        {
            if (GameObject.FindWithTag("MainCamera").GetComponent<Skybox>() != null) GameObject.FindWithTag("MainCamera").GetComponent<Skybox>().material = gameManager.spaceSkybox;
            //Resources.Load<Material>("/Assets/SkyBox/GalaxySkyBox");
        }
        if (gameManager.playerLocation == locationType.Building)
        {
            if (GameObject.FindWithTag("MainCamera").GetComponent<Skybox>() != null) GameObject.FindWithTag("MainCamera").GetComponent<Skybox>().material = Resources.Load<Material>("None");
        }
        //Debug.Log(scene.name + "  " + gameManager.spawnName);
        gameManager.updateTargets = true;
        //gameManager
        GameObject.Find("GameManager").GetComponent<GameManager>()._SpawnPlayer();


        //GameObject.Find("UIManager").GetComponent<SelectionUI>().FindSelectionObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("UIManager").GetComponent<SelectionUI>().isActiveAndEnabled) GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
    }
}
