using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumInventory;

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
    public DCDialog previousDialog;
    public DCDialog nextDialog;

    public Rect parentRect;

    public bool goBack = false;

    public string previousDialogText = "";

    //private
    public bool showDialog = false;

    public InventoryManager inventoryManager;
    public ObjectivesList objectivesList;

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

    void VendorDialog(QI_ItemData[] venderItems, QI_ItemData[] playerItems){
        DCDialog vendorDialog = new DCDialog();
    }

    // Start is called before the first frame update
    void Start() {
        parentRect = new Rect(currentDialog.position.x, currentDialog.position.y, currentDialog.size.x, currentDialog.size.y);
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        objectivesList = GameObject.Find("Objectives").GetComponent<ObjectivesList>();
    }

    // Update is called once per frame
    void Update() {
      //if (currentDialog == null) showDialog = false;            
         if (Input.GetKeyDown(KeyCode.Space) && showDialog){
             
            // nextDialog = currentDialog.nextDialog;
            previousDialog = currentDialog;
            if (currentDialog.keepParentSize) {
                parentRect = new Rect(currentDialog.position.x, currentDialog.position.y, currentDialog.size.x, currentDialog.size.y);
            }
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

        if (currentDialog.dialogType != DialogType.Vendor){

            parentRect = GUILayout.Window(0, parentRect, DialogWindow, "Dialog", GUILayout.ExpandWidth( true ));
        } else { // end if vendor dialog
            Rect windowRect = new Rect(20, 20, 120, 50);
            windowRect = GUILayout.Window(1, windowRect, VendorWindow, "Vendor Window", GUILayout.ExpandWidth( true ));
        }

        }


    void DialogWindow(int windowID){
        if (currentDialog.canGoBack){
            if (GUILayout.Button("< Back")){
                currentDialog = previousDialog;
            }
            
        }
            //GUILayout.BeginArea(new Rect(dialogPosition.x, dialogPosition.y, dialogSize.x, dialogSize.y));
            GUILayout.Label(previousDialogText);

            GUILayout.BeginHorizontal();
            GUILayout.Label(currentDialog.image, GUILayout.Height(64), GUILayout.Width(64));
            GUILayout.Label(currentDialog.dialogName);
            GUILayout.EndHorizontal();
            GUILayout.Label(currentDialog.dialogText);

        if (currentDialog.dialogType == DialogType.TakeItem){
            GUILayout.BeginHorizontal();
                GUILayout.Label(currentDialog.item.Name);
                if (!inventoryManager.hasItem(currentDialog.item.Name)){
                    if (GUILayout.Button("Take Item")){
                        inventoryManager.Pickup(currentDialog.item.Name);
                    }
                } else {
                    GUILayout.Label("Has item already");
                }
            GUILayout.EndHorizontal();
        }
        if (currentDialog.dialogType == DialogType.GiveItem){
            GUILayout.BeginHorizontal();
                GUILayout.Label(currentDialog.item.Name);
                if (GUILayout.Button("Give Item")){

                }
            GUILayout.EndHorizontal();
        }

        if (currentDialog.dialogType == DialogType.AddObjective){
            GUILayout.BeginHorizontal();
                GUILayout.Label(currentDialog.objective.Description);
                //Debug.Log(currentDialog.objective.name);
                if (!objectivesList.isObjectiveEnabled(currentDialog.objective.name)){
                 if (GUILayout.Button("Accept Objective")){
                     objectivesList.enableObjective(currentDialog.objective.name);
                    }
                } else {
                    GUILayout.Label("Objective already activated");
                }
            GUILayout.EndHorizontal();
        }
        if (currentDialog.responses.Count > 0) {

            foreach(DCDialog dialog in currentDialog.responses){
                if (GUILayout.Button(dialog.dialogText)){
                      if (currentDialog.keepParentSize) {
                          parentRect = new Rect(currentDialog.position.x, currentDialog.position.y, currentDialog.size.x, currentDialog.size.y);
                    }
                    previousDialogText = string.Format("You: {0}",dialog.dialogText);
                    previousDialog = currentDialog;
                    currentDialog = dialog.nextDialog;    
                }
            
            }
        } else {
            if (GUILayout.Button("Continue")){
                if (currentDialog.nextDialog != null){
                previousDialog = currentDialog;
                currentDialog = currentDialog.nextDialog;
                } else {
                    showDialog = false;
                }
            }
        }
        //GUILayout.EndArea();
    }
          // Make the contents of the window
    void VendorWindow(int windowID)
    {
        // This button will size to fit the window
        if (GUILayout.Button("Hello World"))
        {
            print("Got a click");
        }
    
}

}
  
