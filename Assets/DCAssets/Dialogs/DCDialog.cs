using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCDialogResponses : MonoBehaviour {
    public string responseText;
    public DCDialog responseDialog;
}

public enum DialogType {
    GiveItem,
    TakeItem,
    AddObjective,
    RemoveObjective
}
// public enum InteracteType {
    
// }
public class DCDialog : MonoBehaviour
{
    public List<DCDialog> responses = new List<DCDialog>();
    public string dialogText;
    public string dialogName;
    //public Image image;
    public Vector2 position;
    public Vector2 size;

    //public int dialogIndex;
    public DCDialog nextDialog;
    public DialogType dialogType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
