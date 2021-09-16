using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTypes
{
    public enum locationType
    {
        Air,
        Building,
        Ground,
        Space,
        Station
    }

    public enum interactType
    {
        Trigger,
        Interactive
    }

    public enum selectionType
    {
        Pickup,
        TakeOff,
        SomethingElse,
        Enemy

    }
    public enum itemType
    {
        Consumable,
        KeyItem
    }
}
