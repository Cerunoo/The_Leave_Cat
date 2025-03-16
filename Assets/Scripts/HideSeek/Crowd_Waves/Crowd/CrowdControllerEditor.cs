using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CrowdController)), CanEditMultipleObjects]
public class CrowdControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CrowdController controller = (CrowdController)target;

        GUILayout.Space(5);

        if (GUILayout.Button("Start in Debug"))
        {
            controller.InitializeStats(null);
        }
    }
}
