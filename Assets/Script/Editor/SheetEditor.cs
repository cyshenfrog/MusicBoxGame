using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class SheetEditor : EditorWindow
{
    int songNumber = 5;
    bool isNew = true;
    static int index;
    static List<SheetData> data = new List<SheetData>();
    static string[] sheetPool;
    void OnEnable()
    {
        sheetPool = new string[songNumber];
        for (int i = 0; i < songNumber; i++)
        {
            data.Add(new SheetData());
            if (data[i].Audio == "")
                sheetPool[i] = "曲目" + i + ": 尚未選擇BGM";
            else
                sheetPool[i] = "曲目" + i + ":" + data[i].Audio;
        }

    }
    [MenuItem("Editor/譜面編輯器")]
    static void OpenWindow()
    {
        GetWindow<SheetEditor>("譜面編輯器");
    }


    GUILayoutOption button = GUILayout.Width(100);

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        index = EditorGUILayout.Popup(index, sheetPool, GUILayout.Width(100));
        if (data.Count != index)
        {
            Debug.Log(data.Count);
            Debug.Log(index);

        }
        if (GUILayout.Button("Load", button))
        {
            if (EditorUtility.DisplayDialog("迷之音", "確定要讀入" + sheetPool[index] + "嘛?", "是", "再考慮"))
            {
                Load();
            }

        }
        EditorGUILayout.EndHorizontal();

        //if (isNew)
        //{
        //    if (GUILayout.Button("Creat", button))
        //    {
        //        data.Add(new SheetData());
        //        Debug.Log(index);
        //        isNew = false;

        //    }

        //}
        Draw();

    }

    void Draw()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("ChooseMusic", button))
            OpenAudioSource();
        EditorGUILayout.LabelField(data[index].Audio);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("New Section", button))
        {
            data[index].section.Add(new Section());
            Debug.Log(data[index].section.Count);

        }

        for (int j = 0; j < data[index].section.Count; j++)
        {
            EditorGUILayout.LabelField("----------------------------------------------------------");
            EditorGUILayout.BeginHorizontal();
            for (int z = 0; z < 8; z++)
            {
                data[index].section[j].Beat[z] = EditorGUILayout.Popup(data[index].section[j].Beat[z], data[index].section[index].Nodetype);
            }
            EditorGUILayout.EndHorizontal();
            
        }
        if (GUILayout.Button("Save", button))
        {
            Save();
        }


    }



    void OpenAudioSource()
    {
        string file = EditorUtility.OpenFilePanel("Open Audio Source", Application.dataPath + "/Resources", "");
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
            AudioClip source = Resources.Load<AudioClip>(file);
            if (source == null)
            {
                Debug.LogError("The file is not a Audio Source.");
                return;
            }
            data[index].Audio = file;
        }
    }


    void Load()
    {
        data[index] = SheetDataManager.Load();
    }

    void Save()
    {
       SheetDataManager.Save(data[index]);
    }

}
