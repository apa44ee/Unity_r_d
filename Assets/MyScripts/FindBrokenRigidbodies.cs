using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FindBrokenRigidbodies : EditorWindow
{
    private List<GameObject> problematicObjects = new List<GameObject>();

    [MenuItem("Tools/Diagnostics/Find Problematic Rigidbodies")]
    public static void ShowWindow()
    {
        GetWindow<FindBrokenRigidbodies>("Rigidbody Checker");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("🔍 Найти проблемные Rigidbody"))
        {
            ScanScene();
        }

        GUILayout.Space(10);

        if (problematicObjects.Count > 0)
        {
            GUILayout.Label($"⚠ Найдено проблем: {problematicObjects.Count}", EditorStyles.boldLabel);

            if (GUILayout.Button("Выделить все"))
            {
                Selection.objects = problematicObjects.ToArray();
            }

            if (GUILayout.Button("Очистить"))
            {
                problematicObjects.Clear();
            }
        }
        else
        {
            GUILayout.Label("✅ Ошибки не найдены.");
        }
    }

    private void ScanScene()
    {
        problematicObjects.Clear();

        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        foreach (var rb in rigidbodies)
        {
            GameObject go = rb.gameObject;
            Collider col = go.GetComponent<Collider>();

            if (col == null)
            {
                Debug.LogWarning($"⚠️ {go.name} — Rigidbody без Collider!", go);
                problematicObjects.Add(go);
                continue;
            }

            if (col is MeshCollider meshCol)
            {
                if (!meshCol.convex && !rb.isKinematic)
                {
                    Debug.LogWarning($"❌ {go.name} — MeshCollider без Convex та Rigidbody не кинематик!", go);
                    problematicObjects.Add(go);
                }

                if (meshCol.sharedMesh == null)
                {
                    Debug.LogWarning($"❌ {go.name} — MeshCollider без Mesh!", go);
                    problematicObjects.Add(go);
                }
            }
        }

        if (problematicObjects.Count == 0)
        {
            Debug.Log("✅ Rigidbody-проблем не знайдено.");
        }
    }
}