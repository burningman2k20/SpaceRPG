﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAngle {

    float myGetAngle (Vector3 _origin, Vector3 target) {
        Vector3 myPos = _origin;
        float angle, angleOpt;
        myPos.y = 0;

        var targetPos = target;
        targetPos.y = 0;

        Vector3 toOther = (myPos - targetPos).normalized;

        angle = Mathf.Atan2 (toOther.z, toOther.x) * Mathf.Rad2Deg + 180;
        angleOpt = atan2Approximation (toOther.z, toOther.x) * Mathf.Rad2Deg + 180;

        Debug.DrawLine (myPos, targetPos, Color.yellow);
        return angle;
    }

    float atan2Approximation (float y, float x) // http://http.developer.nvidia.com/Cg/atan2.html
    {
        float t0, t1, t2, t3, t4;
        t3 = Mathf.Abs (x);
        t1 = Mathf.Abs (y);
        t0 = Mathf.Max (t3, t1);
        t1 = Mathf.Min (t3, t1);
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
        t3 = (Mathf.Abs (y) > Mathf.Abs (x)) ? 1.570796327f - t3 : t3;
        t3 = (x < 0) ? 3.141592654f - t3 : t3;
        t3 = (y < 0) ? -t3 : t3;
        return t3;
    } // Start is called before the first frame update

}