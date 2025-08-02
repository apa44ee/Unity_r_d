using UnityEngine;
using UnityEditor;
using System.IO;

public class EnableReadWriteForMeshes
{
    [MenuItem("Tools/Fix Meshes/Enable Read/Write on All Meshes")]
    public static void EnableReadWrite()
    {
        string[] meshGuids = AssetDatabase.FindAssets("t:Model");

        int fixedCount = 0;

        foreach (string guid in meshGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null && !importer.isReadable)
            {
                importer.isReadable = true;
                importer.SaveAndReimport();
                Debug.Log("✅ Enabled Read/Write on: " + path);
                fixedCount++;
            }
        }

        EditorUtility.DisplayDialog("Done", $"Processed {fixedCount} model(s).", "OK");
    }
}