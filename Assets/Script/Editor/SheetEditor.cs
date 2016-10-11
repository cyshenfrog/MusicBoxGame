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
    SheetData data;
    static List<string> sheetPool = new List<string>();
    void OnEnable()
    {
        //data = SheetDataManager.LoadList();

        data = new SheetData();


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

        index = EditorGUILayout.Popup(index, sheetPool.ToArray(), GUILayout.Width(100));
        if (GUILayout.Button("New Sheet", button))
        {
            sheetPool.Add( OpenAudioSource() );
            
        }
        if (GUILayout.Button("Delete", button))
        {
            if (EditorUtility.DisplayDialog("迷之音", "確定要刪除" + sheetPool[index] + "嘛?", "是", "再考慮"))
            {
                sheetPool.RemoveAt(index);
            }

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
        //Draw();

    }

    void Draw()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("ChooseMusic", button))
            OpenAudioSource();
        EditorGUILayout.LabelField(data.Audio);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("New Section", button))
        {
            data.section.Add(new Section());
            Debug.Log(data.section.Count);

        }

        for (int j = 0; j < data.section.Count; j++)
        {
            EditorGUILayout.LabelField("----------------------------------------------------------");
            EditorGUILayout.BeginHorizontal();
            for (int z = 0; z < 8; z++)
            {
                data.section[j].Beat[z] = EditorGUILayout.Popup(data.section[j].Beat[z], data.section[index].Nodetype);
            }
            EditorGUILayout.EndHorizontal();
            
        }
        if (GUILayout.Button("Save", button))
        {
            //SheetDataManager.Save(data.ToArray());
        }


    }



    string OpenAudioSource()
    {
        string file = EditorUtility.OpenFilePanel("Open Audio Source", Application.dataPath + "/Resources/", "");
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
                return null;
            }
            //檢查是否重複寫譜
            //if (sheetPool.Exists)
            //{

            //}
            //data.Audio = file;
        }
        Debug.Log(file);
        return file;

    }


    void Load()
    {
        data = SheetDataManager.Load();
    }


}
