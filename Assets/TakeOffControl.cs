using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffControl : MonoBehaviour
{
    GameManager gameManager;

    public bool takeOff = true;
    public float _distance;
    public float maxDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        _distance = Vector3.Distance(GameObject.Find(gameManager.currentPrefab.name).transform.position, transform.position);

        // if (gameManager.playerLocation != GameManager.Location_t.Space)
        // {
        if (_distance < maxDistance && gameManager.playerLocation == GameDataTypes.locationType.Ground)
        {
            takeOff = true;
        }
        else
        {
            takeOff = false;
        }
        // }
        // else
        // {
        //     takeOff = true;
        // }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (gameManager.playerLocation == GameDataTypes.locationType.Ground && takeOff)
            {
                gameManager.playerLocation = GameDataTypes.locationType.Space;
                gameManager.canLand = true;
            }
            else if (gameManager.playerLocation == GameDataTypes.locationType.Space)
            {
                gameManager.playerLocation = GameDataTypes.locationType.Ground;
                gameManager.canLand = true;
            }
            gameManager.updateTargets = true;
            gameManager._SpawnPlayer();
        }

    }
}
