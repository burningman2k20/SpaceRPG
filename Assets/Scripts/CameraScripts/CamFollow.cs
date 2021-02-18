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
            offset_move = transform.position - target.position;
        }
        //Debug.Log("offset" + offset_move);
    }

    void Update()
    {
        if (!gameManager.gameStarted) return;
        if (target.Equals(null))
        {
            target= gameManager.findPlayer().transform;
            //if (target.)
            target = GameObject.Find(gameManager.currentPrefab.name + "(Clone)").transform;
            //transform.position = new Vector3(target.position.x, offset_move.y, target.position.y);
            offset_move = transform.position - target.position;
            //Debug.Log("Found player");
            transform.position = target.position + offset_move;
        }
        if (!target.Equals(null))
        {
            //transform.position = new Vector3(target.position.x, offset_move.y, target.position.y);
            transform.position = target.position + offset_move;
        }

    }

    void LateUpdate()
    {

    }

    void Awake()
    {
        //  DontDestroyOnLoad(this.gameObject);
    }
}
