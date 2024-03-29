﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameDataTypes;
using static NewLanding;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public bool gameStarted = false;
	public bool firstRun = false;
    int count = 0;
    public Transform playerShipPosition;
    public string playerStatsJSON;

    public string MenuSceneName;


    [Header ("Managers")]
    [SerializeField] public SpawnManager spawnManager;
    [SerializeField] public PrefabManager prefabManager;
    [SerializeField] public InventoryManager inventoryManager;
    public CharacterManager characterManager;

    //[Header("Stats")]
    // [SerializeField]
    //public PlayerStats playerStats; // = new PlayerStats();
    public CharacterData playerData = new CharacterData ();
    //public SpawnPlayer spawnPlayer;

    //[Header ("Player Prefabs")]
    //[SerializeField] public GameObject groundPrefab;
    //[SerializeField] public GameObject spacePrefab;

    public string spaceSpawn;

    //public GameObject currentPrefab;
    GameObject player;

    public bool shipSave = false;
    public Transform shipSavePosition;
    public Vector3 shipPos;
    public GameObject shipNoMove;
    ShipControls shipControls;

    [Header ("Location")]
    public Transform prefabLocation;
    public Vector3 hoverPlayer = new Vector3 (0, -10, 0);
    public Vector3 besideShip = new Vector3 (0, 0, -15);
    public Vector3 landShip = new Vector3 (0, 6, 0);
    public Vector3 landPerson = new Vector3 (0, 7, 0);

    //public Location_t playerLocation;
    public locationType playerLocation;

    //SmoothFollow2 follow2;
    [Header ("Camera Offsets")]
    public Vector3 mainOffset;
    public Vector3 mapOffset;

    [Header ("Spawn Info")]
    //public GameObject[] SpawnPoints;
    //public string spawnName;
    //public string spaceSpawn;
    //public string spaceScene;
    public string sceneName;
    int spawnIndex = -1;

    public bool isLanding = false;
    public bool canLand = false;

    [Header ("Camera Info")]
    CamFollow follow;
    CamFollow mapFollow;
    Camera mainCamera;
    Camera mapCamera;
    public Material spaceSkybox;
    public float hoverHeight = 9f;
    public float airHoverHeight = 6f;
    public Vector3 buildingCamOffset;
    public Vector3 mainCamOffset;
    public bool updateTargets = false;

    public int _mainOffset = 15;
    public int _buildingOffset = 5;
    private SelectionUI selectionUI;

    int FindSpawnByName (string _name) {
        for (int index = 0; index < spawnManager.SpawnPoints.Length; index++) {
            if (spawnManager.SpawnPoints[index].name == _name) return index;
        }
        return -1;
    }

    public void _SpawnPlayer (string text) {
        // print(text);
        //if (text != null)
        _SpawnPlayer ();
    }

    public void _SpawnPlayer () {
        //spawnManager.refreshSpawnPoints();
        spawnIndex = spawnManager.FindSpawnByName (spawnManager.spawnName);

        // Debug.Log("called _SpawnPlayer() method");
        mainCamera = GameObject.Find ("MainCamera").GetComponent<Camera> ();
        mapCamera = GameObject.Find ("MapCamera").GetComponent<Camera> ();
        selectionUI = GameObject.Find ("UIManager").GetComponent<SelectionUI> ();
        GameObject[] find = GameObject.FindGameObjectsWithTag ("Player");
        foreach (GameObject tmp in find) {
            if (tmp) Destroy (tmp);
        }
        selectionUI.FindSelectionObjects ();
        //SpawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");

        // if (isLanding)
        // {

        spawnIndex = spawnManager.FindSpawnByName (spawnManager.spawnName);
        //spawnName = "none";

        //camOffset = mainCamOffset;
        if (spawnIndex != -1) {
            player = GameObject.FindWithTag ("Player");
            if (playerLocation == locationType.Space) {
                //     //Vector3 spawn = new Vector3()

                player = Instantiate (prefabManager.spacePrefab, spawnManager.SpawnPoints[spawnIndex].transform.position - (hoverPlayer * 2), spawnManager.SpawnPoints[spawnIndex].transform.rotation);
                //currentPrefab = Instantiate(spacePrefab, SpawnPoints[spawnIndex].transform.position, SpawnPoints[spawnIndex].transform.rotation);
                shipControls = player.GetComponent<ShipControls>();
                //characterManager.characterData.shipControls = shipControls;
                shipControls.hoverHeight = hoverHeight;

            }

            if (playerLocation == locationType.Air) {

                player = Instantiate (prefabManager.spacePrefab, spawnManager.SpawnPoints[spawnIndex].transform.position - hoverPlayer, spawnManager.SpawnPoints[spawnIndex].transform.rotation);
                shipControls = player.GetComponent<ShipControls>();
                //characterManager.characterData.shipControls = shipControls;
                shipControls.hoverHeight = airHoverHeight;
                //GameObject.Find(spawnName).SetActive(true);
            }

            if (playerLocation.Equals (locationType.Ground)) {
                landPlayerShip ();
            }

            if (playerLocation == locationType.Building) {
                player = Instantiate (prefabManager.groundPrefab, spawnManager.SpawnPoints[spawnIndex].transform.position, spawnManager.SpawnPoints[spawnIndex].transform.rotation);
                //camOffset = buildingCamOffset;
                follow.offset_move = new Vector3 (0, _buildingOffset, 0);
            }

            if (playerLocation != locationType.Building) {

            }
            //Debug.Log("player prefab -> " + player);

            prefabManager.currentPrefab = player;
            follow.target = player.transform;
            mapFollow.target = player.transform;

            mainCamera.transform.position = player.transform.position + mainOffset;
            mapCamera.transform.position = player.transform.position + mapOffset;
        } else {

        }

    }

    // Start is called before the first frame update
    void Start () {
        prefabManager = GameObject.Find ("PrefabManager").GetComponent<PrefabManager> ();
        spawnManager = GameObject.Find ("SpawnManager").GetComponent<SpawnManager> ();
        inventoryManager = GameObject.Find ("InventoryManager").GetComponent<InventoryManager> ();

        follow = GameObject.Find ("MainCamera").GetComponent<CamFollow> ();
        mapFollow = GameObject.Find ("MapCamera").GetComponent<CamFollow> ();
        //playerStats = GameObject.Find("GameManager").GetComponent<PlayerStats>();

        selectionUI = GameObject.Find ("UIManager").GetComponent<SelectionUI> ();
        if (!selectionUI.Equals (null)) selectionUI.FindSelectionObjects ();
    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        //spawnName = "none";
        //SpawnPlayer();
        print ("Loaded : " + scene.name);

    }

    void FixedUpdate () {
        // playerStats.playerData._position = prefabLocation.position;
        // playerStats.playerData._rotation = prefabLocation.rotation;
        // playerStats.playerData._spacePrefabName = spacePrefab.name;
        // playerStats.playerData._groundPrefabName = groundPrefab.name;
        // playerStats.playerData._tag = prefabLocation.gameObject.tag;
        // playerStats.playerData._location = playerLocation;
        // playerStats.playerData.sceneName = SceneManager.GetActiveScene().name;

        // if (!updateTargets.Equals(false)){
        //     //if (selectionUI != null)
        //GameObject.Find("UIManager").GetComponentInChildren<SelectionUI>().enabled = true;
        selectionUI = GameObject.Find ("UIManager").GetComponent<SelectionUI> ();
        //Debug.Log(selectionUI);
        selectionUI.FindSelectionObjects ();
        //this.currentPrefab = locatePlayerPrefab();
        //Debug.Log(locatePlayerPrefab());
        //playerStatsJSON.name = "currentPlayer.name";
        //playerStatsJSON.score = "100";
        //playerStats.playerData.
        //playerStatsJSON = JsonUtility.ToJson(playerStats);
        //Debug.Log(playerStatsJSON);
        //Debug.Log(currentPlayer);
        // updateTargets = false;
        // }
    }

    public GameObject findPlayer () {
        GameObject find = GameObject.Find ("Player");
        if (find == null) {
            Debug.Log ("no player found");
            //();
            //find = GameObject.Find("Player");
        } else {
            Debug.Log ("player found");
            //return find;
        }

        return find;

    }

    public void landPlayerShip () {
        //Debug.Log("landing ship");

        follow.offset_move = new Vector3 (0, _mainOffset, 0);
		spawnIndex = spawnManager.FindSpawnByName(spawnManager.spawnName);
        //GameObject shipNoMove = Instantiate(spacePrefab, SpawnPoints[spawnIndex].transform.position - landShip, SpawnPoints[spawnIndex].transform.rotation);
        if (!shipSave) {
            shipNoMove = Instantiate (prefabManager.spacePrefab, spawnManager.SpawnPoints[spawnIndex].transform.position, spawnManager.SpawnPoints[spawnIndex].transform.rotation);
			shipSave = true;
        } else {
            shipNoMove = Instantiate (prefabManager.spacePrefab, shipPos, Quaternion.identity);
            shipSave = false;
        }
        //Destroy(shipNoMove.GetComponent<ClickToMove>());
        Destroy (shipNoMove.GetComponent<ShipControls> ());
        Destroy (shipNoMove.GetComponentInChildren<Shoot> ());
        //Destroy(shipNoMove.GetComponent<LandControl>());
        Destroy (shipNoMove.GetComponent<TakeOffControl> ());
        //shipNoMove.tag = "untagged";
        Vector3 down = shipNoMove.transform.TransformDirection (Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast (shipNoMove.transform.position, down, out hit)) {
            //distance = hit.distance;
            //Debug.Log("This distance is " + hit.distance);
            //float speed = 1.0F;
            //float step = speed * Time.deltaTime; // calculate distance to move
            //shipNoMove.transform.position = Vector3.MoveTowards(shipNoMove.transform.position, (shipNoMove.transform.position - new Vector3(0, hit.distance, 0)), step);
            Vector3 m_MyPosition = new Vector3 (1, 1, 1);
            m_MyPosition.Set (shipNoMove.transform.position.x, shipNoMove.transform.position.y - (float) hit.distance, shipNoMove.transform.position.z);
            shipNoMove.transform.position = m_MyPosition;
        }
        //GameObject.Find(spawnName).SetActive(false);
        shipNoMove.GetComponent<Rigidbody> ().useGravity = true;
        //shipNoMove.tag = null;
        //player = Instantiate(playerPrefab, SpawnPoints[spawnIndex].transform.position + besideShip - landPerson, SpawnPoints[spawnIndex].transform.rotation);
        prefabManager.currentPrefab = Instantiate (prefabManager.groundPrefab, spawnManager.SpawnPoints[spawnIndex].transform.position + besideShip, spawnManager.SpawnPoints[spawnIndex].transform.rotation);
        //characterManager.characterData.tankController = prefabManager.currentPrefab.GetComponent<SimpleTankController>();
        follow.target = locatePlayerPrefab ().transform;
        mapFollow.target = locatePlayerPrefab ().transform;

    }
    public GameObject locatePlayerPrefab () {
        GameObject[] found = GameObject.FindGameObjectsWithTag ("Player");
        foreach (GameObject tmp in found) {
            if (tmp.GetComponent<ShipControls> () != null || tmp.GetComponent<SimpleTankController> () != null) {
                //playerData.data.myGameObject = tmp;
                prefabManager.currentPrefab = tmp;
                return tmp;
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update () {

        if (!gameStarted) return;
        if (Input.GetKeyDown (KeyCode.Z)) { // load
            //playerData.LoadData ();
			characterManager.ReadFile("");
        }

        if (Input.GetKeyDown (KeyCode.C)) { // save
            //playerData.SaveData ();
			characterManager.WriteFile("");
        }

        //Debug.Log(findPlayer());
        if (isLanding) {
            //landPlayerShip();
            if (playerLocation == locationType.Ground) {
                prefabManager.currentPrefab.GetComponent<SimpleTankController> ().playerControl = false;
                isLanding = false;
            }
        } else {
            prefabManager.currentPrefab.GetComponent<SimpleTankController> ().playerControl = true;
            count = 0;
        }

        if (locatePlayerPrefab () == null) {
            _SpawnPlayer ();
            prefabManager.currentPrefab = findPlayer();
        }
        if (GameObject.FindWithTag ("Player") != null) {
            prefabLocation = prefabManager.currentPrefab.transform;

            //GameObject.FindWithTag("Player").transform;
        }
        //if (prefabLocation == null) spawnName = "none";

        if (Input.GetMouseButton (1) || Input.GetKeyDown (KeyCode.Escape)) {
            //movable = false;
            //destinationPosition = new Vector3(this.myTransform.position.x, this.myTransform.position.y, this.myTransform.position.z);
            GameObject.Find ("UIManager").GetComponentInChildren<SelectionUI> ().enabled = true;
            if (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target != null) {
                selectionUI.FindSelectionObjects ();
                if (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.GetComponent<Target> () != null) GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.GetComponent<Target> ().indicator.Activate (false);
                //if (GameObject.Find("UIManager").GetComponent<SelectionUI>().target.GetComponent<Selection>() != null) GameObject.Find("UIManager").GetComponent<SelectionUI>().target.GetComponent<Selection>().select = false;
            }
            GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target = null;
        }
    }

    ///<summary>
    /// Checks if items need to be respawned on scene load
    /// </summary>
    void checkRespawnItems () {

    }

    public void newLandingCode () {
        Debug.Log ("New Landing Code Executing");

        playerShipPosition = prefabManager.currentPrefab.transform;
        Vector3 down = playerShipPosition.TransformDirection (Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast (playerShipPosition.position, down, out hit)) {
            //distance = hit.distance;
            //Debug.Log("This distance is " + hit.distance);
            //float speed = 1.0F;
            //float step = speed * Time.deltaTime; // calculate distance to move
            //shipNoMove.transform.position = Vector3.MoveTowards(shipNoMove.transform.position, (shipNoMove.transform.position - new Vector3(0, hit.distance, 0)), step);
            Vector3 m_MyPosition = new Vector3 (1, 1, 1);
            m_MyPosition.Set (playerShipPosition.position.x, playerShipPosition.position.y - (float) hit.distance * 2, playerShipPosition.position.z);
            //shipNoMove.transform.position = m_MyPosition;
            GameObject temp = Instantiate (prefabManager.spacePrefab, m_MyPosition, Quaternion.identity);
            //temp.GetComponent<Rigidbody>().useGravity = true;
            temp.GetComponent<ShipControls> ().playerControl = false;
        }
        //GameObject.Find(spawnName).SetActive(false);

        //isLanding = true;
    }

    void Awake () {
        selectionUI = GameObject.Find ("UIManager").GetComponent<SelectionUI> ();
        //Variables.App.Set("name", "David Crispin");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        selectionUI.FindSelectionObjects ();
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().FindSelectionObjects();
        //Debug.Log("Awake");
        //if (spawnManager.findPlayer() == null)  _SpawnPlayer();
        // spawnName = "none";
        follow = GameObject.Find ("MainCamera").GetComponent<CamFollow> ();
        mapFollow = GameObject.Find ("MapCamera").GetComponent<CamFollow> ();

        follow.target = prefabManager.currentPrefab.transform;
        mapFollow.target = prefabManager.currentPrefab.transform;
        //DontDestroyOnLoad(this.gameObject);
    }
}
