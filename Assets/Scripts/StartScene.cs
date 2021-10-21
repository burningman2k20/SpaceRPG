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

	public bool firstRun = false;

    GameManager gameManager;
	SpawnManager spawnManager;
	ObjectivesList objectiveList;
	InventoryManager inventoryManager;

    public string MenuSceneName;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		objectiveList = GameObject.Find("Objectives").GetComponent<ObjectivesList>();
		inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
		gameManager.gameStarted = gameStarted;
		GameObject.Find("UIManager").GetComponent<ObjectiveUI>().showUI = showObjectiveUI;
		//objectiveList.objectiveList.RemoveAll()
		//objectiveList = GameObject.Find("Objectives").GetComponent<ObjectivesList>();
		//objectiveList.objectiveList = GameObject.FindGameObjectsWithTag("Objective");
		//objectiveList.loadObjectives = true;
//		objectiveList.WriteFile();
		//objectiveList.ReadFile();
        gameManager.enabled = true;
        GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
        spawnManager.SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
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
 		StartCoroutine(load());

        //GameObject.Find("UIManager").GetComponent<SelectionUI>().FindSelectionObjects();
    }

	IEnumerator load(){
		objectiveList.playerObjectiveList = objectiveList.ReadFile("");
		foreach(MainObjectiveList obj in objectiveList.playerObjectiveList){
		GameObject go = GameObject.Find(obj._objectiveObjectName);
		Objective objective;
		if (go != null) {
			objective = go.GetComponent<Objective>();

			if (obj.completed){
				objective.Status = ObjectiveStatus.Achieved;
			} else {
					//GUILayout.Box(ObjectiveStatus.Pending.ToString());
				objective.Status = ObjectiveStatus.Pending;
			}
		} // endif go != null
		}
		inventoryManager.ReadFile("");
		yield return new WaitForSeconds(1.0f);
	}

	void OnSceneLoaded(){

	}

void FixedUpdate(){
// 	if (firstRun){
// 		OnSceneLoaded();
// 		firstRun = false;
// }
}
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape)){
            string menu = gameManager.MenuSceneName;
            Destroy(GameObject.Find("Controllers"));
            Destroy(GameObject.Find("MainCamera"));
			 SceneManager.LoadScene(menu);
        }
        if (!GameObject.Find("UIManager").GetComponent<SelectionUI>().isActiveAndEnabled) GameObject.Find("UIManager").GetComponent<SelectionUI>().enabled = true;
    }
}
