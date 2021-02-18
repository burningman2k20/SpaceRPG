using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingArea : MonoBehaviour
{
    GameManager gameManager;
    [Header("Spawn Data")]
    [SerializeField] public string spawnName;
    [SerializeField] public string airSpawn;
    [SerializeField] public string groundSpawn;
    [SerializeField] public string spaceSpawn;
    [SerializeField] public string sceneName;

    [Header("Show UI?")]
    [SerializeField] public bool displayUI = false;
    public bool isDisabled = true;
    // void OnGUI()
    // {

    //     if (displayUI && !isDisabled)
    //     {
    //         if (gameManager.playerLocation == GameDataTypes.locationType.Space || gameManager.playerLocation == GameDataTypes.locationType.Air)
    //         {
    //             GUILayout.Button("Land");
    //         }
    //         else if (gameManager.playerLocation == GameDataTypes.locationType.Space || gameManager.playerLocation == GameDataTypes.locationType.Ground)
    //         {
    //             GUILayout.Button("Take Off");
    //         }
    //     }
    // }

    void OnCollisionEnter(Collision collision)
    {
        //gameManager.spawnName = spawnName;
        //gameManager.isLanding = true;
    }

    void OnCollisionExit(Collision collision)
    {
        //        gameManager.spawnName = spawnName;
        //gameManager.isLanding = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindWithTag("Player").GetComponent<ShipControls>()) GameObject.FindWithTag("Player").GetComponent<ShipControls>().landingControl = true;
        displayUI = true;
        gameManager.spawnName = spawnName;
        gameManager.sceneName = sceneName;
        //if (GameObject.FindWithTag("GameManager").GetComponent<GameManager>()
        if (gameManager.playerLocation == GameDataTypes.locationType.Space) gameManager.spaceSpawn = gameObject.name;
        // Debug.Log("entered landing area -> " + gameObject.name);
        //gameManager.isLanding = true;
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>()
        gameManager.canLand = true;
    }
    void OnTriggerStay(Collider other)
    {
        //displayUI = true;
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>()
        gameManager.spawnName = spawnName;
        gameManager.sceneName = sceneName;
        //Debug.Log("staying in landing area -> " + gameObject.name);
        gameManager.canLand = true;
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().isLanding = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (GameObject.FindWithTag("Player").GetComponent<ShipControls>()) GameObject.FindWithTag("Player").GetComponent<ShipControls>().landingControl = false;
        displayUI = false;
        //Debug.Log("exiting landing area " + gameObject.name);
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>()
        gameManager.spawnName = "none";
        if (gameManager.playerLocation == GameDataTypes.locationType.Air) gameManager.spawnName = gameManager.spaceSpawn;
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>()
        gameManager.sceneName = "none";
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>()
        gameManager.canLand = false;
        //gameManager.isLanding = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //gameManager.spawnName;
        //spawnName = this.gameObject.name;
        //airSpawn = this.gameObject.name + "Air";
        //groundSpawn = this.gameObject.name + "Ground";

    }

    // Update is called once per frame
    void Update()
    {

    }
}