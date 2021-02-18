using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType;
    private Image indicatorImage;
    private Text distanceText;
    //private Color originalColor;

    /// <summary>
    /// Gets if the game object is active in hierarchy.
    /// </summary>
    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }

    /// <summary>
    /// Gets the indicator type
    /// </summary>
    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    void Awake()
    {
        indicatorImage = transform.GetComponent<Image>();
        distanceText = transform.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    // public void SwitchColor(Color clr){
    //     if (indicatorImage.color == clr) {
    //         indicatorImage.color = originalColor;
    //        } else {
    //             indicatorImage.color = clr;
    //         }
    //     }
    // }
    public void SetImageColor(Color color)
    {
        indicatorImage.color = color;
    }

    /// <summary>
    /// Sets the distance text for the indicator.
    /// </summary>
    /// <param name="value"></param>
    public void SetDistanceText(float value)
    {
        //distanceText.width = 150;
        if (value >= 0){
            //;string test = string.format("%d", Math,floor(value));
            //Debug.Log(test);
            if (value < 10) distanceText.text =  String.Format("{0:0.#}m", value / 10);
            if (value >= 10) distanceText.text = String.Format("{0:0.#}k", value / 100);
        } else {
            distanceText.text = "";
        }
        //distanceText.text = value >= 0 ? Mathf.Floor(value) + " m" : "";
    }

    /// <summary>
    /// Sets the distance text rotation of the indicator.
    /// </summary>
    /// <param name="rotation"></param>
    public void SetTextRotation(Quaternion rotation)
    {
        distanceText.rectTransform.rotation = rotation;
    }

    /// <summary>
    /// Sets the indicator as active or inactive.
    /// </summary>
    /// <param name="value"></param>
    public void Activate(bool value)
    {
        transform.gameObject.SetActive(value);
    }

    /// <summary>
    /// Returns if the indicator is active or inactive.
    /// </summary>
        public bool isActive()
    {
        return gameObject.active;
    }
}

public enum IndicatorStatusType{
    Complete, Pending
}
public enum IndicatorType
{
    BOX,
    ARROW,
    NONE
}
