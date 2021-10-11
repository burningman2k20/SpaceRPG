using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameDataTypes;

public class SceneChanger : MonoBehaviour
{
    public enum interact_t
    {
        Trigger, Interactive
    }

    public float distance;
    public float interact_distance = 1;
    public string interactKey = "enter";
    public interactType interact;

    public locationType location;

    public bool Visable = false;

    public string spawnName;
    //public GameObject playerPrefab;

    public string SceneName;

    public GameManager gameManager;
	public PrefabManager prefabManager;
	public SpawnManager spawnManager;

    public bool gui_visable = false;

    void OnTriggerEnter(Collider other)
    {
        if (interact == interactType.Trigger)
        {

            switch (location)
            {
                case locationType.Building:


                    gameManager.playerLocation = locationType.Building;
                    break;
                case locationType.Ground:


                    gameManager.playerLocation = locationType.Ground;
                    break;
                default:
                    break;

            }

            spawnManager.sceneName = SceneName;
            spawnManager.spawnName = spawnName;
            SceneManager.LoadScene(SceneName);

            //}
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (interact == GameDataTypes.interactType.Trigger)
        {
            //Debug.Log("trigger exited -> " + gameObject.name);
        }
    }


    void OnGUI()
    {

        if (gui_visable)
        {
            GUILayout.Button("Interact");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prefabManager.currentPrefab.Equals(null)) return;
        distance = Vector3.Distance(prefabManager.currentPrefab.transform.position, this.transform.position);

        if (distance < interact_distance)
        {
            gui_visable = true;
            if (interact == GameDataTypes.interactType.Interactive && Input.GetKeyDown(interactKey))
            //&& Input.GetKeyDown(KeyCode.Return))
            {
                //Debug.Log("interact key pressed");
                if (SceneName != "")
                {
                    gameManager.playerLocation = GameDataTypes.locationType.Building;
                    GameObject tempObject = GameObject.Find(prefabManager.spacePrefab.name + "(Clone)");
                    if (tempObject != null)
                    {
                        gameManager.shipSave = true;

                        gameManager.shipPos.x = tempObject.transform.position.x;
                        gameManager.shipPos.y = tempObject.transform.position.y;
                        gameManager.shipPos.z = tempObject.transform.position.z;
                        gameManager.shipSavePosition = tempObject.transform;
                    }
                    //gm.UpdateData=true;
                    // gm.spawnIndex=0;
                    spawnManager.spawnName = spawnName;
                    SceneManager.LoadScene(SceneName);
                    //gm.SpawnPlayer();
                }
            }
        }
        else
        {
            gui_visable = false;
        }

        //if (!Visable) {
        gameObject.GetComponent<MeshRenderer>().enabled = Visable;

        if (interact == GameDataTypes.interactType.Trigger)
        {
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
        }

    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
    }
}
