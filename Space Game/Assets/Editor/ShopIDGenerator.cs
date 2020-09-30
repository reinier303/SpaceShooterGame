using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class ShopIDGenerator : EditorWindow
{



    [MenuItem("Tools/Shop ID Generator")]
    private static void Init()
    {
        var window = (ShopIDGenerator)GetWindow(typeof(ShopIDGenerator));
        window.minSize = new Vector2(400, 300);
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Generate IDs"))
        {
            EditorGUILayout.LabelField("Shop ID Generator", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("\n");
            Object[] ScriptableItems = Resources.LoadAll("ShopItems", typeof(ScriptableShopItem));

            int index = 0;

            foreach (ScriptableShopItem item in ScriptableItems)
            {
                if (item.IDSet)
                {
                    index++;
                }          
            }

            foreach (ScriptableShopItem item in ScriptableItems)
            {
                if(!item.IDSet)
                {
                    item.ID = index;
                    item.IDSet = true;
                    Debug.Log(item.ShopItemName + "ID set to:" + item.ID);
                    index++;
                }
            }
        }
    }
}
