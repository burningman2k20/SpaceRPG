using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GUI_Utils
    {
        Rect PosToRect(Vector2 pos, Rect bounds, Rect inset)
        {
            Rect i = inset;
            Rect rect = new Rect(0, 0, 0, 0);
            rect.x = bounds.x + (bounds.width * pos.x) + i.x;
            rect.y = bounds.y + (bounds.height * pos.y) + i.y;
            rect.width = i.width;
            rect.height = i.height;
            return rect;
        }
    }
}