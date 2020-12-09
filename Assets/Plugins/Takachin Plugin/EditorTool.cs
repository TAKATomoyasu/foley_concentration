using UnityEditor;
using UnityEngine;
public static class TobimaruEditorTool {
#if UNITY_EDITOR
    [MenuItem ("Tools/Next To Parent &p")]
    static void NextToParent () {
        Transform selection = Selection.activeGameObject.transform;
        Transform parent = selection.transform.parent;
        selection.parent = parent.parent;
        selection.transform.SetSiblingIndex(parent.GetSiblingIndex());
    }
    #endif
}