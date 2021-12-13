using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCDialogController : MonoBehaviour
{
    //private 
    public List<DCDialog> dialogs = new List<DCDialog>();

    //private
    public Vector2 dialogPosition = new Vector2(50,50);
    //private
    public Vector2 dialogSize = new Vector2(75,250);
    //private
    public DCDialog currentDialog;
    public DCDialog nextDialog;

    //private
    public bool showDialog = false;

    void addDialog(DCDialog dialog){
        dialogs.Add(dialog);
    }

    void removeDialog(string name){ 
        dialogs.Remove(getDialog(name));
    }

    void setCurrentDialog(string searchName){
        currentDialog = getDialog(searchName);
    }

    void setCurrentDialog(DCDialog dialog){
        currentDialog = dialog;
    }

    DCDialog getCurrentDialog(){
        return currentDialog;
    }

    DCDialog getDialog(string searchName){
        for (int x = 0;x < dialogs.Count; x++){
            if (dialogs[x].dialogName == searchName) return dialogs[x];
        }
        return null;
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
      //if (currentDialog == null) showDialog = false;            
         if (Input.GetKeyDown(KeyCode.Space) && showDialog){
             
            // nextDialog = currentDialog.nextDialog;
            currentDialog = nextDialog;
            if (nextDialog == null || currentDialog == null) showDialog = false;            
        }
    }

    void FixedUpdate(){
        //if (currentDialog == null) showDialog = false;
    }

    void displayDialog(string searchName){
        if (searchName == "") return;
            currentDialog = getDialog(searchName);
            showDialog = true;
    }

    void OnGUI(){

        if (!showDialog) return;

        dialogPosition = currentDialog.position;
        dialogSize = currentDialog.size;

        GUILayout.BeginArea(new Rect(dialogPosition.x, dialogPosition.y, dialogSize.x, dialogSize.y));
        GUILayout.Label(currentDialog.dialogName);
        GUILayout.Label(currentDialog.dialogText);

        if (currentDialog.dialogType == DialogType.TakeItem){
            GUILayout.BeginHorizontal();
                GUILayout.Label(currentDialog.item.Name);
                GUILayout.Button("Take Item");
            GUILayout.EndHorizontal();
        }
        if (currentDialog.dialogType == DialogType.GiveItem){
            GUILayout.BeginHorizontal();
                GUILayout.Label(currentDialog.item.Name);
                GUILayout.Button("Give Item");
            GUILayout.EndHorizontal();
        }

        if (currentDialog.dialogType == DialogType.AddObjective){
            GUILayout.BeginHorizontal();
                GUILayout.Label(currentDialog.objective.Description);
                GUILayout.Button("Accept Objective");
            GUILayout.EndHorizontal();
        }
        if (currentDialog.responses.Count > 0) {

            foreach(DCDialog dialog in currentDialog.responses){
                if (GUILayout.Button(dialog.dialogText)){
                    currentDialog = dialog.nextDialog;    
                }
            
            }
        }
        GUILayout.EndArea();

       
    }
}
