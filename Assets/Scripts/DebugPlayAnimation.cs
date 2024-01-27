using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Animation))]
public class DebugPlayAnimation : MonoBehaviour
{
    public void Play() => GetComponent<Animation>().Play();
}

#if UNITY_EDITOR
[CustomEditor(typeof(DebugPlayAnimation))]
public class DebugPlayAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "m_Script");
        if (GUILayout.Button("Play animation clip")) ((DebugPlayAnimation)target).Play();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
