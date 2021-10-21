// Removed two depreciated functions.  Used Deg2Rad and back again because I don't know what I'm doing.  Anyone is welcome to clean this up properly.  -WarpZone

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using static GameDataTypes;
using QuantumTek.QuantumInventory;

// Put this on a rigidbody object and instantly
// have 2D spaceship controls like OverWhelmed Arena
// that you can tweak to your heart's content.

[RequireComponent(typeof(Rigidbody))]
public class ShipControls : MonoBehaviour
{
    [Header("Motion")]
    //public engine_class engine_info;
    [SerializeField] public float hoverHeight = 3F;
    [SerializeField] public float hoverHeightStrictness = 1F;
    [SerializeField] public float forwardThrust = 5000F;
    [SerializeField] public float backwardThrust = 2500F;
    [SerializeField] public float bankAmount = 0.1F;
    [SerializeField] public float bankSpeed = 0.2F;
    [SerializeField] public Vector3 bankAxis = new Vector3(-1F, 0F, 0F);
    [SerializeField] public float turnSpeed = 8000F;

    [SerializeField] public Vector3 forwardDirection = new Vector3(1F, 0F, 0F);

    [SerializeField] public float mass = 5F;

    // positional drag
    [SerializeField] public float sqrdSpeedThresholdForDrag = 25F;
    [SerializeField] public float superDrag = 2F;
    [SerializeField] public float fastDrag = 0.5F;
    [SerializeField] public float slowDrag = 0.01F;

    // angular drag
    [SerializeField] public float sqrdAngularSpeedThresholdForDrag = 5F;
    [SerializeField] public float superADrag = 32F;
    [SerializeField] public float fastADrag = 16F;
    [SerializeField] public float slowADrag = 0.1F;

    [SerializeField] public bool playerControl = true;

    [SerializeField] public bool landingControl = false;

    public Engines engine;//=new engine_class();
    public Weapons weapon;
    //public weapon_class weapon;//=new weapon_class();

    float bank = 0F;
    GameManager gameManager;
	SpawnManager spawnManager;
    CharacterManager characterManager;
    LandingArea landing;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter -> " + other.gameObject.name);
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit -> " + other.gameObject.name);
    }

    void SetPlayerControl(bool control)
    {
        playerControl = control;
    }

    // public engine_class returnEngine(){
    //   return engine;
    // }
    //
    // public weapon_class returnWeapon(){
    //   return weapon;
    // }
    public void setEngine(QI_ItemData new_engine){
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager> ();
		if (new_engine == null){
            characterManager.characterData.shipEngine = null;
            engine = null;
			forwardThrust = 0;
	        backwardThrust = 0;
			return;
		}
		string engine_name = new_engine.ItemPrefab.gameObject.name;
        //characterManager.characterData.shipEngineName = engine_name;
		Engines changeEngine =  new_engine.ItemPrefab.gameObject.GetComponent<Engines>();
        //characterManager.characterData.shipEngine = changeEngine;
		//Resources.Load<Engines>("Prefabs/ShipComponents/Engines/" + engine_name) ;
		//Debug.Log(engine_name);
		//Engines changeEngine = new_engine.ItemPrefab.gameObject.GetComponent<Engines>();
        engine = changeEngine;
        characterManager.characterData.shipEngine = engine;
        forwardThrust = engine.forwardThrust;
        backwardThrust = engine.backwardThrust;
    }

    public void setWeapon(QI_ItemData new_weapon){
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager> ();
		if (new_weapon == null){
			weapon = null;
            characterManager.characterData.shipWeapon = null;
            //forwardThrust = 0;
            //backwardThrust = 0;
            return;
		}
		string weapon_name = new_weapon.ItemPrefab.gameObject.name;
		Weapons changeWeapon =  new_weapon.ItemPrefab.gameObject.GetComponent<Weapons>();
		//characterManager.characterData.shipWeaponName = weapon_name;
        //characterManager.characterData.shipWeapon = changeWeapon;
        //Resources.Load<Engines>("Prefabs/ShipComponents/Engines/" + engine_name) ;
		//Debug.Log(engine_name);
		//Engines changeEngine = new_engine.ItemPrefab.gameObject.GetComponent<Engines>();
        weapon = changeWeapon;
        characterManager.characterData.shipWeapon = weapon;
        //forwardThrust = engine.forwardThrust;
        //backwardThrust = engine.backwardThrust;
    }
    //
    // void setEngine(string new_engine){
    //     engine_class changeEngine=Resources.Load<engine_class>("Prefabs/World/Engines/" + new_engine) ;
    //     engine = changeEngine;
    //     forwardThrust=engine.forward_thrust;
    //     backwardThrust=engine.backward_thrust;
    // }

    void Start()
    {
        GetComponent<Rigidbody>().mass = mass;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager> ();
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().FindSelectionObjects();
        //forwardThrust=forward_thrust;
        //backwardThrust=backward_thrust;

        //engine=Resources.Load<engine_class>("Prefabs/World/Engines/engine1") ;

        //transform.rotation=Quaternion.;
    }

    void Awake(){
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager> ();
        //weapon = characterManager.characterData.groundWeapon;
        //engine = characterManager.characterData.shipEngine;
    }

    void FixedUpdate()
    {

        if (Mathf.Abs(thrust) > 0.01F)
        {
            if (GetComponent<Rigidbody>().velocity.sqrMagnitude > sqrdSpeedThresholdForDrag)
                GetComponent<Rigidbody>().drag = fastDrag;
            else
                GetComponent<Rigidbody>().drag = slowDrag;
        }
        else
            GetComponent<Rigidbody>().drag = superDrag;

        if (Mathf.Abs(turn) > 0.01F)
        {
            if (GetComponent<Rigidbody>().angularVelocity.sqrMagnitude > sqrdAngularSpeedThresholdForDrag)
                GetComponent<Rigidbody>().angularDrag = fastADrag;
            else
                GetComponent<Rigidbody>().angularDrag = slowADrag;
        }
        else
            GetComponent<Rigidbody>().angularDrag = superADrag;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hoverHeight, transform.position.z), hoverHeightStrictness);

        float amountToBank = GetComponent<Rigidbody>().angularVelocity.y * bankAmount;

        bank = Mathf.Lerp(bank, amountToBank, bankSpeed);

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation *= Mathf.Deg2Rad;
        rotation.x = 0F;
        rotation.z = 0F;
        rotation += bankAxis * bank;
        //transform.rotation = Quaternion.EulerAngles(rotation);
        rotation *= Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotation);
    }

    float thrust = 0F;
    float turn = 0F;

    void Thrust(float t)
    {
        thrust = Mathf.Clamp(t, -1F, 1F);
    }

    void Turn(float t)
    {
        turn = Mathf.Clamp(t, -1F, 1F) * turnSpeed;
    }

    bool thrustGlowOn = false;


    void Update()
    {
        float theThrust = thrust;

        if (Input.GetKeyDown(KeyCode.K) && gameManager.playerLocation == locationType.Air) {

            gameManager.newLandingCode();
        }

        if (Input.GetKeyDown(KeyCode.L) && gameManager.canLand && spawnManager.spawnName != "none")
        {

            // print(gameManager.spawnName);
            //if (gameManager.spawnName == "none" || gameManager.spawnName == "") gameManager.spawnName = "StartingPoint";
            switch (gameManager.playerLocation)
            {
                case locationType.Air:
                    gameManager.playerLocation = locationType.Ground;
                    gameManager.landPlayerShip();
                    break;
                case locationType.Space:
                    gameManager.playerLocation = locationType.Air;
                    //gameManager.spaceSpawn = "StartingPoint";

                    if (gameManager.sceneName == "none")
                    {
                        gameManager.sceneName = "Ground";
                    }

                    SceneManager.LoadScene(spawnManager.sceneName);

                    break;
            }
            Debug.Log(gameManager.playerLocation);
             gameManager.GetComponent<GameManager>()._SpawnPlayer();//"same map");
            gameManager.updateTargets = true;
        }
		//&& landingControl
        if (Input.GetKeyDown(KeyCode.T)  && gameManager.playerLocation != locationType.Space)
        {
            //print(gameManager.spawnName);
            gameManager.playerLocation = locationType.Space;

            //gameManager.spaceSpawn = "StartingPoint";
            spawnManager.spawnName = gameManager.spaceSpawn;
            print(gameManager.sceneName);
            // if (gameManager.sceneName == "none" || gameManager.sceneName == "")
            // {
                gameManager.sceneName = "Space";

            // }
            //gameManager.spawnName = gameManager.spaceSpawn;
            //gm.locate=GameManager.location_t.Ground;
            //gm.UpdateData=true;
            //gm.spawnIndex=0;
            //gm.spawnName=spawnName;
            SceneManager.LoadScene(gameManager.sceneName);

            //gameManager.SpawnPlayer("same map");
        }

        if (playerControl)
        {
            thrust = Input.GetAxis("Vertical");
            turn = Input.GetAxis("Horizontal") * turnSpeed;
			//Debug.Log(string.Format("thrust {0} --- turn {1}", thrust, turn));
        }

        if (thrust > 0F)
        {
            theThrust *= forwardThrust;
            if (!thrustGlowOn)
            {
                thrustGlowOn = !thrustGlowOn;
                BroadcastMessage("SetThrustGlow", thrustGlowOn, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            theThrust *= backwardThrust;
            if (thrustGlowOn)
            {
                thrustGlowOn = !thrustGlowOn;
                BroadcastMessage("SetThrustGlow", thrustGlowOn, SendMessageOptions.DontRequireReceiver);
            }
        }

        GetComponent<Rigidbody>().AddRelativeTorque(Vector3.up * turn * Time.deltaTime);
        GetComponent<Rigidbody>().AddRelativeForce(forwardDirection * theThrust * Time.deltaTime);
    }
}
