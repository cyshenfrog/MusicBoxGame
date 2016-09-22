using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class SheetEditor : EditorWindow{
    static SheetData data = new SheetData();
    static string filename;
    Vector2 scrollViewPoz;
    void OnEnable()
    {
        scrollViewPoz = new Vector2();
    }

    [MenuItem("Editor/SheetEditor")]
    static void OpenWindow()
    {
        GetWindow(typeof(SheetEditor));
    }


    GUILayoutOption button = GUILayout.Width(100);
    GUILayoutOption lable = GUILayout.MaxWidth(20);
    GUILayoutOption beat = GUILayout.MaxWidth(20);
    void OnGUI()
    {

        if (GUILayout.Button("ChooseMusic", button))
            OpenAudioSource();

        scrollViewPoz = GUILayout.BeginScrollView(scrollViewPoz);
        EditorGUILayout.BeginHorizontal();
        filename = EditorGUILayout.TextField("Loaded Filename: ", filename, GUILayout.Width(300f));

        if (GUILayout.Button("Load", GUILayout.Width(150f)))
        {
            if (EditorUtility.DisplayDialog("迷之音", "確定要讀入" + filename + "嘛?", "是", "再考慮"))
            {
                Load();
            }
        }
        EditorGUILayout.EndHorizontal();

        //to draw the buttons
        Draw();
        GUILayout.EndScrollView();
        if (GUILayout.Button("Add Chapter"))
            data.section.Add(new Section());


    }

    void Load()
    {

    }

    void Save()
    {

    }

    void Draw()
    {

        int Sectionmount = 1;
        for (int i = 0; i < Sectionmount; i++)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(i + ".", lable);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("WWWW", beat))
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("V", lable);
            }
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
            data.Audio = file;
        }
    }




}
