using System.Collections;
using System.Collections.Generic;
using QuantumTek.QuantumInventory;
using UnityEngine;
using UnityEngine.UIElements;

public class DCDialogResponses : MonoBehaviour {
    public string responseText;
    public DCDialog responseDialog;
}

public enum DialogType {
    Nothing,
    GiveItem,
    TakeItem,
    AddObjective,
    RemoveObjective,
    Vendor
}
// public enum InteracteType {

// }
public class DCDialog : MonoBehaviour {
    public List<DCDialog> responses = new List<DCDialog> ();
    public string dialogText;
    public string dialogName;
    public Image dialogImage;
    public Texture image;
    //public Image image;
    public Vector2 position;
    public Vector2 size;

    public bool keepParentSize = true;

    //public int dialogIndex;
    public DCDialog nextDialog;
    public DialogType dialogType;

    public QI_ItemData item;
    public int itemCount = 0;
    public int responseIndex = 0;

    public bool canGoBack = false;

    public Objective objective;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}