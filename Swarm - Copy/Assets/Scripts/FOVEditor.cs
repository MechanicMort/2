﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FOV))]
public class FOVEditor : Editor
{

    private void OnSceneGUI()
    {
        FOV fov = (FOV)target;
        Handles.color = Color.green;
        Handles.DrawWireArc(fov.transform.position, Vector3.up,Vector3.forward,360,fov.viewRadious);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadious);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadious);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fov.visibleTargetsList)
        {
            Handles.DrawLine(fov.transform.position,visibleTarget.position);
        }
    }
}
