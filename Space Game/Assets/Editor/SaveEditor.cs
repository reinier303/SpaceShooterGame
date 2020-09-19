using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveEditor : EditorWindow
{



    [MenuItem("Tools/Save Editor")]
    private static void Init()
    {
        var window = (SaveEditor)GetWindow(typeof(SaveEditor));
        window.minSize = new Vector2(400, 300);
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Delete Save"))
        {
            EditorGUILayout.LabelField("Save Editor", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("\n");
            string path = Application.persistentDataPath + "/saveData";
            DirectoryInfo directory = new DirectoryInfo(path);
            if (Directory.Exists(path))
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                Debug.Log("Files at: " + path + " deleted.");
            }
            else 
            {
                Debug.Log("No file found at: " + path);                
            }
        }
    }
}
