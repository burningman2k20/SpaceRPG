using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapons weapon;
    //public GameObject projectile;
    public Transform projectileOrigin;
    public Transform frontMount;
    public Transform rearMount;
    public Transform leftMount;
    public Transform rightMount;
    public Projectile projectileData;
    public bool shoot = true;
    public Vector3 direction = Vector3.forward;
    public float angleFront;
    public float angleRear;
    public float angleLeft;
    public float angleRight;
    public Rect angleInfo = new Rect(0, 0, 0, 0);
    public bool IsFacing;
    bool facingFront = false;
    bool facingRear = false;
    void Start()
    {

    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        //if (Input.GetKeyDown (KeyCode.K)) {
        GUILayout.BeginArea(angleInfo);
        //GUILayout.Box("Front " + angleFront.ToString());
        //GUILayout.Box("facing front?  " + facingFront);
        //GUILayout.Box("facing rear?  " + facingRear);
        //GUILayout.Box (angleLeft.ToString ());
        //GUILayout.Box (angleRight.ToString ());
        GUILayout.EndArea();
        //  }
    }
    // Update is called once per frame
    void Update()
    {

        projectileData = weapon.projectile.GetComponent<Projectile>();
        /*if (Input.GetKeyDown (KeyCode.Q)) {
            shoot = !shoot;
            GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target = null;
        }*/
        //if (GameObject.Find("UIManager").GetComponent<SelectionUI>().target != null)
        //{
        //float _angle = myGetAngle (frontMount.position, GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform.position);

        //projectileOrigin.LookAt (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform);

        //if (GameObject.Find("UIManager").GetComponent<SelectionUI>().target){
        // facingFront = IsFacingObject(frontMount, GameObject.Find("UIManager").GetComponent<SelectionUI>().target.transform);
        // facingRear = IsFacingObject(rearMount, GameObject.Find("UIManager").GetComponent<SelectionUI>().target.transform);

        //bool facingLeft = IsFacingObject (leftMount, GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform);
        //bool facingRight = IsFacingObject (rightMount, GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform);
        //shoot = false;

        // if (facingFront)
        // {
        // projectileOrigin = frontMount;
        // // projectileOrigin.LookAt (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform);
        // direction = projectileOrigin.forward;
        //facingFront = false;
        //shoot = true;
        //     }
        //  	if (facingRear)
        //     {
        //         projectileOrigin = rearMount;
        // 		projectileOrigin.LookAt (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform);
        //         direction = projectileOrigin.forward;
        // 		facingRear = false;
        //         //shoot = true;
        //     }
        // } else {
        projectileOrigin = frontMount;
        //projectileOrigin.rotation = new Quaternion(0, 0, 0, 0);
        //facingFront = false;
        //facingRear = false;
        direction = projectileOrigin.forward;
        //}
        //if (GameObject.Find ("GameManager").GetComponent<GameManager> ().playerLocation == GameDataTypes.locationType.Air) direction = Vector3.down;

        //}
        if (Input.GetKeyDown(KeyCode.Period))
        {

            //if (GameObject.Find("UIManager").GetComponent<SelectionUI>().target != null)
            //{
            //Transform temp = projectileOrigin;
            //temp.LookAt (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform);
            GameObject bullet = Instantiate(weapon.projectile.gameObject, projectileOrigin.position + direction, projectileOrigin.rotation) as GameObject;

            //bullet.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform.position) * projectileData.speed;

            //projectileOrigin.LookAt (GameObject.Find ("UIManager").GetComponent<SelectionUI> ().target.transform);
            bullet.GetComponent<Rigidbody>().AddForce(projectileOrigin.forward * projectileData.speed);
            //facingRear = false;
            //facingFront = false;
            //projectileOrigin.transform.rotation = Quaternion.identity;
            Destroy(bullet.gameObject, 25f);
            //}
            /* else {
                            projectileOrigin.LookAt (Vector3.up);
                            bullet.GetComponent<Rigidbody> ().AddForce (transform.forward * projectileData.speed);
                        }*/
        }

    }

    float myGetAngle(Vector3 _origin, Vector3 target)
    {
        Vector3 myPos = _origin;
        float angle, angleOpt;
        myPos.y = 0;

        var targetPos = target;
        //targetPos.y = 0;

        Vector3 toOther = (myPos - targetPos).normalized;

        angle = Mathf.Atan2(toOther.z, toOther.x) * Mathf.Rad2Deg + 180;
        angleOpt = atan2Approximation(toOther.z, toOther.x) * Mathf.Rad2Deg + 180;

        Debug.DrawLine(myPos, targetPos, Color.yellow);
        return angleOpt;
    }

    bool IsFacingObject(Transform _origin, Transform _target)
    {
        // Check if the gaze is looking at the front side of the object
        Vector3 forward = _origin.forward;
        Vector3 toOther = (_target.position - _origin.position).normalized;

        if (Vector3.Dot(forward, toOther) < 0.7f)
        {
            Debug.Log("Not facing the object");
            return false;
        }

        Debug.Log("Facing the object");
        return true;
    }
    float atan2Approximation(float y, float x) // http://http.developer.nvidia.com/Cg/atan2.html
    {
        float t0, t1, t2, t3, t4;
        t3 = Mathf.Abs(x);
        t1 = Mathf.Abs(y);
        t0 = Mathf.Max(t3, t1);
        t1 = Mathf.Min(t3, t1);
        t3 = 1f / t0;
        t3 = t1 * t3;
        t4 = t3 * t3;
        t0 = -0.013480470f;
        t0 = t0 * t4 + 0.057477314f;
        t0 = t0 * t4 - 0.121239071f;
        t0 = t0 * t4 + 0.195635925f;
        t0 = t0 * t4 - 0.332994597f;
        t0 = t0 * t4 + 0.999995630f;
        t3 = t0 * t3;
        t3 = (Mathf.Abs(y) > Mathf.Abs(x)) ? 1.570796327f - t3 : t3;
        t3 = (x < 0) ? 3.141592654f - t3 : t3;
        t3 = (y < 0) ? -t3 : t3;
        return t3;
    } // Start is called before the first frame update
}
