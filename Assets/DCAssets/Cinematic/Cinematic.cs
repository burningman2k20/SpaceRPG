using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CinematicType {
    //ZoomedInCamera,
   // ZoomedOutCamera,
    FollowPlayer,
    FollowTarget
}
public class Cinematic : MonoBehaviour
{
    public string cinematicTitle;
    //public int dialogIndex;
    private Cinematic nextCinematic;
    public CinematicType cimematicType;
    private Transform followTarget;
    private Transform cameraPosition;
    private Camera savedCameraData;

    void setFollowTarget(Transform target){
        followTarget = target;
    }

    Transform getFollowTarget(){
        return followTarget;
    }

    void setNextCinematic(Cinematic next){
        nextCinematic = next;
    }

    Cinematic getNextCinematic(){
        return nextCinematic;
    }
}
