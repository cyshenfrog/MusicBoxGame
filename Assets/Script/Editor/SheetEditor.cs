using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class SheetEditor : EditorWindow{
    SheetData data;


    private static void initial()
    {

    }

    [MenuItem("Editor/SheetEditor")]
    public static void OpenWindow()
    {
        GetWindow<SheetEditor>("SheetEditor");
    }


    GUILayoutOption button = GUILayout.Width(100);

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save", button))
            OpenAudioSource();
    }

        void OpenAudioSource()
    {
        string file = EditorUtility.OpenFilePanel("Open Image", Application.dataPath + "/Resources", "");
        if (file != "")
        {
            string resPath = Application.dataPath + "/Resources/";
            if (!file.StartsWith(resPath))
            {
                Debug.LogError("The path is not in the \"Resources\"");
            }
            file = file.Remove(0, resPath.Length);
            int lastDotPos = file.LastIndexOf(".");
            string[] dotSplit = file.Split('.');
            file = file.Remove(lastDotPos, dotSplit[dotSplit.Length - 1].Length + 1);
            //file=file.Remove (file.Length - 4, 4);
            Sprite s = Resources.Load<Sprite>(file);
            if (s == null)
            {
                Debug.LogError("The file is not a image.");
                return;
            }
            data.Audio = file;
        }
    }


}
