using UnityEngine;
using UnityEditor;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Diagnostics/Find Missing Scripts in Scene")]
    public static void FindMissingScriptsInScene()
    {
        int missingCount = 0;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning("🚨 Missing script in: " + GetFullPath(go), go);
                    missingCount++;
                }
            }
        }

        EditorUtility.DisplayDialog("Готово", $"Знайдено {missingCount} обєктів з відсутніми скриптами.", "ОК");
    }

    static string GetFullPath(GameObject obj)
    {
        return obj.transform.parent == null ? obj.name : GetFullPath(obj.transform.parent.gameObject) + "/" + obj.name;
    }
}