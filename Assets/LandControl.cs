// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LandControl : MonoBehaviour
// {
//     GameManager gameManager;
//     // Start is called before the first frame update
//     void Start()
//     {
//         gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
//     }

//     // Update is called once per frame
//     void Update()
//     {


//         if (Input.GetKeyDown(KeyCode.P))
//         {
//             Vector3 pos = transform.position;
//             pos.y = Terrain.activeTerrain.SampleHeight(transform.position);
//             //Debug.Log(pos);
//             //transform.position = pos;
//             float speed = 1f;
//             float step = speed * Time.deltaTime; // calculate distance to move
//             transform.position = Vector3.MoveTowards(transform.position, pos, step);
//         }
//         if (Input.GetKeyDown(KeyCode.L) && gameManager.isLanding)
//         {

//             if (gameManager.playerLocation == GameManager.Location_t.Space)
//             {
//                 gameManager.playerLocation = GameManager.Location_t.Ground;
//                 gameManager.isLanding = true;
//             }
//             else if (gameManager.playerLocation == GameManager.Location_t.Ground)
//             {
//                 gameManager.playerLocation = GameManager.Location_t.Space;
//                 gameManager.isLanding = true;
//             }
//             gameManager.SpawnPlayer();
//         }

//         if (!gameManager.isLanding)
//         {
//             //Debug.Log("cant land here");
//         }

//     }
// }
