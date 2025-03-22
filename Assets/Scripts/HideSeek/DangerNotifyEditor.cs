#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DangerNotify))]
public class DangerNotifyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DangerNotify dangerController = (DangerNotify)target;

        GUILayout.Space(5);

        if (GUILayout.Button("Left Danger"))
        {
            dangerController.CallLeftDanger();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Right Danger"))
        {
            dangerController.CallRightDanger();
        }
    }
}

#endif