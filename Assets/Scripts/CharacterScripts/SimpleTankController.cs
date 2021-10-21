using UnityEngine;
using static GameDataTypes;
using QuantumTek.QuantumInventory;

[RequireComponent (typeof (CharacterController))]
public class SimpleTankController : MonoBehaviour {
    public GameManager gameManager;
	public SpawnManager spawnManager;
	public PrefabManager prefabManager;
    public CharacterManager characterManager;

    // Tank Controller
    public enum MoveDirection { Forward, Reverse, Stop }

    [Header ("Motion")]

    [SerializeField] float topForwardSpeed = 3.0f;
    [SerializeField] float topReverseSpeed = 1.0f;

    [Tooltip ("Rate at which top speed is reached.")]
    [SerializeField] float acceleration = 3.0f;

    [Tooltip ("Rate at which speed is lost when not accelerating.")]
    [SerializeField] float deceleration = 2.0f;

    [Tooltip ("Rate at which speed is lost when braking (input opposite of current direction).")]
    [SerializeField] float brakingDeceleration = 4.0f;

    [Tooltip ("Maintain speed when no input is provided.")]
    [SerializeField] bool stickyThrottle = false;

    [Tooltip ("Delay between change of direction when sticky throttle is enabled.")]
    [SerializeField] float stickyThrottleDelay = 0.35f;

    [SerializeField] float gravity = 10.0f;

    [Header ("Rotation")]

    [SerializeField] float topForwardTurnRate = 1.0f;
    [SerializeField] float topReverseTurnRate = 2.0f;
    [SerializeField] float stoppedTurnRate = 2.0f;

	[SerializeField] public bool playerControl = true;

    [SerializeField] public float _distance;
    [SerializeField] public float maxDistance = 1f;
    float currentSpeed;
    float currentTopSpeed;
    MoveDirection currentDirection = MoveDirection.Stop;
    bool isBraking = false;
    bool isAccelerating = false;
    float stickyDelayCount = 9999f;
    CharacterController m_Controller;
    Transform m_Transform;
    public Weapons weapon;

    Transform obj1; // = GameObject.Find(gameManager.prefabLocation.name).transform;

    Transform obj2; //= transform;
    
    
     public void setWeapon(QI_ItemData new_weapon){
		if (new_weapon == null){
            characterManager.characterData.groundWeapon = null;
            weapon = null;
			//forwardThrust = 0;
	        //backwardThrust = 0;
			return;
		}
		string weapon_name = new_weapon.ItemPrefab.gameObject.name;
		Weapons changeWeapon =  new_weapon.ItemPrefab.gameObject.GetComponent<Weapons>();
        characterManager.characterData.groundWeaponName = weapon_name;
		//Resources.Load<Engines>("Prefabs/ShipComponents/Engines/" + engine_name) ;
		//Debug.Log(engine_name);
		//Engines changeEngine = new_engine.ItemPrefab.gameObject.GetComponent<Engines>();
        weapon = changeWeapon;
        //forwardThrust = engine.forwardThrust;
        //backwardThrust = engine.backwardThrust;
    }
    private void Awake () {
        // Performance optimization - cache transform reference.
        m_Transform = GetComponent<Transform> ();

        m_Controller = GetComponent<CharacterController> ();
        //spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        //prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        currentTopSpeed = topForwardSpeed;
        //weapon = characterManager.characterData.groundWeapon;
    }

    void OnTriggerEnter (Collider other) {
        if (GameObject.Find (other.gameObject.name).GetComponent<LandingArea> ()) GameObject.Find (other.gameObject.name).GetComponent<LandingArea> ().displayUI = true;
        //	    Debug.Log("enter -> " + other.gameObject.name);
    }
    void OnTriggerExit (Collider other) {
        if (GameObject.Find (other.gameObject.name).GetComponent<LandingArea> ()) GameObject.Find (other.gameObject.name).GetComponent<LandingArea> ().displayUI = false;
        //Debug.Log("exit -> " + other.gameObject.name);
    }

    public void Update () {

        if (Input.GetKeyDown (KeyCode.T) && spawnManager.spawnName != "none") {
            if (gameManager.playerLocation == locationType.Building) return;

			if (GameObject.Find (prefabManager.spacePrefab.name + "(Clone)")) {
				obj1 = GameObject.Find (prefabManager.spacePrefab.name + "(Clone)").transform;
				obj2 = transform;
				_distance = Vector3.Distance (obj1.position, obj2.position);
				Debug.Log(_distance);
			if (_distance <= maxDistance) {

					gameManager.playerLocation = locationType.Air;

	                gameManager._SpawnPlayer (); //("same map");
                    gameManager.updateTargets = true;


	            }
            }
        }

    }
    private void FixedUpdate () {
        if (gameManager.prefabLocation != null) {
            if (GameObject.Find (gameManager.prefabLocation.name).transform) {
                obj1 = GameObject.Find (gameManager.prefabLocation.name).transform;
                obj2 = transform;
                _distance = Vector3.Distance (obj1.position, obj2.position);
            } else {
                return;
            }
        }
        // Direction to move this update.
        Vector3 moveDirection = Vector3.zero;

        // Direction requested this update.
        MoveDirection requestedDirection = MoveDirection.Stop;

        if (m_Controller.isGrounded) {
            // Simulate loss of turn rate at speed.
            float currentTurnRate = Mathf.Lerp (currentDirection == MoveDirection.Forward ?
                topForwardTurnRate : topReverseTurnRate, stoppedTurnRate, 1 - (currentSpeed / currentTopSpeed));

            Vector3 angles = m_Transform.eulerAngles;
            angles.y += (Input.GetAxis ("Horizontal") * currentTurnRate);
            m_Transform.eulerAngles = angles;

            // Based on input, determine requested action.
            if (Input.GetAxis ("Vertical") > 0) // Requesting forward.
            {
                requestedDirection = MoveDirection.Forward;
                isAccelerating = true;
            } else {
                if (Input.GetAxis ("Vertical") < 0) // Requesting reverse.
                {
                    requestedDirection = MoveDirection.Reverse;
                    isAccelerating = true;
                } else {
                    requestedDirection = currentDirection;
                    isAccelerating = false;
                }
            }

            isBraking = false;

            if (currentDirection == MoveDirection.Stop) {
                stickyDelayCount = stickyDelayCount + Time.deltaTime;

                // If we are not sticky throttle or if we have hit the delay then change direction.
                if (!stickyThrottle || (stickyDelayCount > stickyThrottleDelay)) {
                    // Make sure we can go in the requested direction.
                    if (((requestedDirection == MoveDirection.Reverse) && (topReverseSpeed > 0)) ||
                        ((requestedDirection == MoveDirection.Forward) && (topForwardSpeed > 0))) {
                        currentDirection = requestedDirection;
                    }
                }
            } else {
                // Requesting a change of direction, but not stopped so we are braking.
                if (currentDirection != requestedDirection) {
                    isBraking = true;
                    isAccelerating = false;
                }
            }

            // Setup top speeds and move direction.
            if (currentDirection == MoveDirection.Forward) {
                moveDirection = Vector3.forward;
                currentTopSpeed = topForwardSpeed;
            } else {
                if (currentDirection == MoveDirection.Reverse) {
                    moveDirection = -1 * Vector3.forward;
                    currentTopSpeed = topReverseSpeed;
                } else {
                    if (currentDirection == MoveDirection.Stop) {
                        moveDirection = Vector3.zero;
                    }
                }
            }

            if (isAccelerating) {
                // If we haven't hit top speed yet, accelerate.
                if (currentSpeed < currentTopSpeed) {
                    currentSpeed = currentSpeed + (acceleration * Time.deltaTime);
                }
            } else {
                // If we are not accelerating and still have some speed, decelerate.
                if (currentSpeed > 0) {
                    // Adjust deceleration for braking and implement sticky throttle.
                    float currentDecelerationRate = isBraking ? brakingDeceleration : (!stickyThrottle ? deceleration : 0);
                    currentSpeed = currentSpeed - (currentDecelerationRate * Time.deltaTime);
                }
            }

            // If our speed is below zero, stop and initialize.
            if ((currentSpeed < 0) || ((currentSpeed == 0) && (currentDirection != MoveDirection.Stop))) {
                SetStopped ();
            } else {
                // Limit the speed to the current top speed.
                if (currentSpeed > currentTopSpeed) {
                    currentSpeed = currentTopSpeed;
                }
            }

            moveDirection = m_Transform.TransformDirection (moveDirection);
        }

        // Implement gravity so we can stay grounded.
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        moveDirection.z = moveDirection.z * (Time.deltaTime * currentSpeed);
        moveDirection.x = moveDirection.x * (Time.deltaTime * currentSpeed);
        m_Controller.Move (moveDirection);
    }

    private void SetStopped () {
        currentSpeed = 0;
        currentDirection = MoveDirection.Stop;
        isAccelerating = false;
        isBraking = false;
        stickyDelayCount = 0;
    }

    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager> ();
		spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager> ();
		prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager> ();
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager> ();
        //GameObject.Find("UIManager").GetComponent<SelectionUI>().FindSelectionObjects();
    }
}
