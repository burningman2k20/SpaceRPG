using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType {
  Trigger,
  KeyPress
}
public class Interact : MonoBehaviour {

  public DCDialog dialog;
  public InteractType type = InteractType.KeyPress;

  public void OnCollisionEnter (Collision collision) {

    if (type == InteractType.Trigger) {
      Debug.Log ("collided with talker");
     startDialog();
      // gameObject.GetComponent<BoxCollider>().isTrigger = true;
    } else {

      // gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }

  }
  public void Start () {

  }

  public void Update () {
    if (type == InteractType.KeyPress) {
      if (Input.GetKeyDown (KeyCode.Return)) {
        // Debug.Log ("key pressed with talker");
        startDialog ();
      }
      //  gameObject.GetComponent<BoxCollider>().isTrigger = true;
    } else {
      //  gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
  }

  void startDialog () {
    DCDialogController dialogController = GameObject.Find ("DCDialogController").GetComponent<DCDialogController> ();
    dialogController.currentDialog = dialog;
    dialogController.showDialog = true;
  }

  public void OnGUI () {

  }

}