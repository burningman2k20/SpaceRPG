// /*
//  * Esse Script movimenta o GameObject quando você clica ou
//  * mantém o botão esquerdo do mouse apertado.
//  *
//  * Para usá-lo, adicione esse script ao gameObject que você quer mover
//  * seja o Player ou outro
//  *
//  * Autor: Vinicius Rezendrix - Brasil
//  * Data: 11/08/2012
//  *
//  * This script moves the GameObject when you
//  * click or click and hold the LeftMouseButton
//  *
//  * Simply attach it to the GameObject you wanna move (player or not)
//  *
//  * Autor: Vinicius Rezendrix - Brazil
//  * Data: 11/08/2012
//  *
//  */

// using UnityEngine;
// using System.Collections;

// public class ClickToMove : MonoBehaviour
// {
//     private Transform myTransform;              // this transform
//     private Vector3 destinationPosition;        // The destination Point
//     private float destinationDistance;          // The distance between myTransform and destinationPosition

//     public float moveSpeed;                     // The Speed the character will move
//     public bool movable = false;
//     public float maxSpeed = 10f;
//     public float groundSpeed = 3f;
//     public float spaceSpeed = 10f;

//     GameManager gameManager;


//     void Start()
//     {
//         myTransform = transform;                            // sets myTransform to this GameObject.transform
//         destinationPosition = myTransform.position;         // prevents myTransform reset
//         gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
//     }

//     void OnCollisionEnter(Collision collision)
//     {
//         movable = false;
//         // foreach (ContactPoint contact in collision.contacts)
//         // {
//         //     Debug.DrawRay(contact.point, contact.normal, Color.white);
//         // }
//         // if (collision.relativeVelocity.magnitude > 2)
//         //     audioSource.Play();
//     }
//     void Update()
//     {
//         if (gameManager.playerLocation == GameManager.Location_t.Ground)
//         {
//             maxSpeed = groundSpeed;
//         }
//         else
//         {
//             maxSpeed = spaceSpeed;
//         }
//         if (Input.GetKeyDown(KeyCode.M))
//         {
//             movable = true;
//         }
//         //Debug.Log("Space key was pressed.");


//         // keep track of the distance between this gameObject and destinationPosition
//         destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);

//         if (destinationDistance < .5f)
//         {       // To prevent shakin behavior when near destination
//             moveSpeed = 0;
//         }
//         else if (destinationDistance > .5f)
//         {           // To Reset Speed to default
//             moveSpeed = maxSpeed;
//         }


//         if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Escape))
//         {
//             movable = false;
//             //destinationPosition = new Vector3(this.myTransform.position.x, this.myTransform.position.y, this.myTransform.position.z);
//             GameObject.Find("UIManager").GetComponent<SelectionUI>().target = null;
//         }
//         // Moves the Player if the Left Mouse Button was clicked
//         if (Input.GetMouseButtonDown(0) && movable && GUIUtility.hotControl == 0)
//         {

//             Plane playerPlane = new Plane(Vector3.up, myTransform.position);
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//             float hitdist = 0.0f;



//             if (playerPlane.Raycast(ray, out hitdist))
//             {
//                 Vector3 targetPoint = ray.GetPoint(hitdist);
//                 destinationPosition = ray.GetPoint(hitdist);
//                 Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
//                 myTransform.rotation = targetRotation;
//             }
//             movable = false;
//         }

//         // Moves the player if the mouse button is hold down
//         else if (Input.GetMouseButton(0) && movable && GUIUtility.hotControl == 0)
//         {

//             Plane playerPlane = new Plane(Vector3.up, myTransform.position);
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//             float hitdist = 0.0f;

//             if (playerPlane.Raycast(ray, out hitdist))
//             {
//                 Vector3 targetPoint = ray.GetPoint(hitdist);
//                 destinationPosition = ray.GetPoint(hitdist);
//                 Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
//                 myTransform.rotation = targetRotation;
//             }
//             movable = false;
//             //myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
//         }

//         // To prevent code from running if not needed
//         if (destinationDistance > .5f)
//         {
//             myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
//         }

//         //} else	      { return; }

//     }

// }
