using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset_move;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //gameManager.enabled= true;
        if (target)
        {
            //offset_move = transform.position - target.position;
        }
        //Debug.Log("offset" + offset_move);
    }

    void Update()
    {
        if (gameManager == null) gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (!gameManager.gameStarted) return;

		//Debug.Log(target);

        if (target.Equals(null))
        {
            //target = gameManager.findPlayer().transform;
            //if (target.)
            target = gameManager.locatePlayerPrefab().transform;
			Debug.Log(target);
            //transform.position = new Vector3(0, offset_move.y, 0);
			//if (gameManager.playerLocation == locationType.Ground){
			//	target.position.x = -3;
			//} else {
			//	target.position.x = 0;
			//}
            //offset_move = transform.position - target.position;
			//offset_move = new Vector3(0, offset_move.y, 0);
            //Debug.Log("Found player");
            transform.position = target.position + offset_move;
        }
        if (!target.Equals(null))
        {
			target = gameManager.locatePlayerPrefab().transform;
            //transform.position = new Vector3(target.position.x, offset_move.y, target.position.y);
            transform.position = target.position + offset_move;
        }

    }

    void LateUpdate()
    {

    }

    void Awake()
    {
		if (target)
        {
            //offset_move = transform.position - target.position;
        }
        //  DontDestroyOnLoad(this.gameObject);
    }
}
