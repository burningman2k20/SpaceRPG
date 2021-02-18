using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;

public class ClearSight : MonoBehaviour
{
    public float DistanceToPlayer = 5.0f;
    GameManager gameManager;
    public Material TransparentMaterial = null;
    public float FadeInTimeout = 0.6f;
    public float FadeOutTimeout = 0.2f;
    public float TargetTransparency = 0.3f;
    public float buildingHeight = 10f;
    public float groundHeight = 15f;
    public float airHeight = 20f;
    public float spaceHeight = 20f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        RaycastHit[] hits; // you can also use CapsuleCastAll()
                           // TODO: setup your layermask it improve performance and filter your hits.
        float hover = 0f;

        switch (gameManager.playerLocation)
        {
        case locationType.Space:
                DistanceToPlayer = spaceHeight;
                hover = gameManager.hoverHeight;
                break;
        case locationType.Air:
                DistanceToPlayer = airHeight;
                hover = gameManager.airHoverHeight;
                break;
	case locationType.Building:
                    DistanceToPlayer = buildingHeight;
                    hover = gameManager.airHoverHeight;
                    break;
        default:
                hover = 3.5f;
                DistanceToPlayer = groundHeight;//gameManager.mainOffset;
                break;
        }


        hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer - hover);
        foreach (RaycastHit hit in hits)
        {

            Renderer R = hit.collider.GetComponent<Renderer>();
            //Debug.Log(hit.collider.gameObject.name);
            if (R == null)
            {
                continue;
            }
            // no renderer attached? go to next hit
            // TODO: maybe implement here a check for GOs that should not be affected like the player
            AutoTransparent AT = R.GetComponent<AutoTransparent>();
            if (AT == null) // if no script is attached, attach one
            {
                AT = R.gameObject.AddComponent<AutoTransparent>();
                AT.TransparentMaterial = TransparentMaterial;
                AT.FadeInTimeout = FadeInTimeout;
                AT.FadeOutTimeout = FadeOutTimeout;
                AT.TargetTransparency = TargetTransparency;
            }
            AT.BeTransparent(); // get called every frame to reset the falloff
        }
    }
}
