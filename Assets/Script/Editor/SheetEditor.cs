using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class SheetEditor : EditorWindow
{
    int SectionNo;
    bool isNew = true;
    static int index;
    SheetData SheetData;
    EditorData EditorData;
    static int[] Section = new int[8];
    static string[] Nodetype = new string[4] { "  ", "Red", "Green", "Blue" };
    void OnEnable()
    {
        //data = SheetDataManager.LoadList();

        SheetData = new SheetData();
        if (SheetDataManager.isEditorDataExist())
        {
            EditorData = SheetDataManager.Load();
        }
        else
        {
            EditorData = new EditorData();
            SheetDataManager.Save(EditorData);
        }
        SectionNo = 0;

    }
    [MenuItem("Editor/譜面編輯器")]
    static void OpenWindow()
    {
        GetWindow<SheetEditor>("譜面編輯器");
    }



    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        index = EditorGUILayout.Popup(index, EditorData.sheetPool.ToArray(), GUILayout.Width(100));
        //Debug.Log(index);
        if (GUILayout.Button("開新譜面", GUILayout.Width(100)))
        {
            EditorData.SheetNumber++;
            EditorData.sheetPool.Add("Enpty"+ EditorData.SheetNumber);
            SheetDataManager.Save(EditorData);
        }

        if (EditorData.SheetNumber != -1 && SheetDataManager.isSavedataExist(index))
        {
            if (GUILayout.Button("讀取之前的進度", GUILayout.Width(150)))
            {
                if (EditorUtility.DisplayDialog("迷之音", "確定要讀入" + EditorData.sheetPool[index] + "嘛?", "是", "再考慮"))
                {
                    SheetData = SheetDataManager.Load(index);
                    if (SheetData.Ch1.Capacity > 0)
                    {
                        SectionNo = 1;
                    }
                }
            }
        }
        if (EditorData.SheetNumber != -1 )
        {
            //if (GUILayout.Button("Clean this Sheet", GUILayout.Width(150)))
            //{
            //    if (EditorUtility.DisplayDialog("迷之音", "確定要清除所有譜面內容嘛?", "是", "再考慮"))
            //    {
            //        SheetData.Audio = OpenAudioSource();
            //        EditorData.sheetPool[index] = SheetData.Audio;
            //        SheetDataManager.Save(EditorData);
            //    }

            //}
            if (GUILayout.Button("刪除此樂譜", GUILayout.Width(100)))
            {
                if (EditorUtility.DisplayDialog("迷之音", "確定要刪除" + EditorData.sheetPool[index] + "嘛?", "是", "再考慮"))
                {
                    EditorData.SheetNumber--;
                    EditorData.sheetPool.RemoveAt(index);
                    SheetDataManager.Save(EditorData);
                    AssetDatabase.DeleteAsset(Application.dataPath + @"\Sheet" + index.ToString());
                }
            }
        }


        EditorGUILayout.EndHorizontal();
        Draw();

        //if (isNew)
        //{
        //    if (GUILayout.Button("Creat", button))
        //    {
        //        data.Add(new SheetData());
        //        Debug.Log(index);
        //        isNew = false;
        //    }
        //}

    }

    void Draw()
    {
        if (EditorData.SheetNumber != -1)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("選擇音樂檔", GUILayout.Width(100)))
            {
                SheetData.Audio = OpenAudioSource();
                EditorData.sheetPool[index] = SheetData.Audio;
                SheetDataManager.Save(EditorData);
                SheetDataManager.Save(SheetData, index);

            }
            EditorGUILayout.LabelField(SheetData.Audio);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("BPM");
            SheetData.Bpm = EditorGUILayout.IntSlider(SheetData.Bpm, 0, 300, GUILayout.Width(150));
            if (GUILayout.Button("新增小節", GUILayout.Width(100)))
            {
                SheetData.Ch1.AddRange(Section);
                SheetData.Ch2.AddRange(Section);
                SheetData.Ch3.AddRange(Section);
                if (SheetData.Ch1.Capacity / 8 == 1)
                {
                    SectionNo = 1;
                }

            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("上一小節", GUILayout.Width(100)))
            {
                if (SectionNo>1)
                {
                    SectionNo--;
                }
            }
            EditorGUILayout.LabelField("第"+ SectionNo + "小節(共"+ SheetData.Ch1.Count / 8 + "小節)");

            if (GUILayout.Button("下一小節", GUILayout.Width(100)))
            {
                if (SectionNo < SheetData.Ch1.Count / 8)
                {
                    SectionNo++;
                }
            }


            EditorGUILayout.EndHorizontal();

            if (SectionNo > 0)
            {
                for (int j = ((SectionNo * 8) - 8); j < SectionNo * 8; j++)
                {
                    if (j == (SectionNo * 8) - 8)
                    {
                        EditorGUILayout.LabelField("----------------------------------------------------------");
                        EditorGUILayout.BeginHorizontal();
                    }
                    SheetData.Ch1[j] = EditorGUILayout.Popup(SheetData.Ch1[j], Nodetype);
                    if(j == (SectionNo * 8) -1)
                        EditorGUILayout.EndHorizontal();
                }

                for (int k = (SectionNo * 8) - 8; k < SectionNo * 8; k++)
                {
                    if (k == (SectionNo * 8) - 8)
                    {
                        EditorGUILayout.LabelField("----------------------------------------------------------");
                        EditorGUILayout.BeginHorizontal();
                    }
                    SheetData.Ch2[k] = EditorGUILayout.Popup(SheetData.Ch2[k], Nodetype);
                    if (k == (SectionNo * 8) - 1)
                        EditorGUILayout.EndHorizontal();
                }

                for (int l = (SectionNo * 8) - 8; l < SectionNo * 8; l++)
                {
                    if (l == (SectionNo * 8) - 8)
                    {
                        EditorGUILayout.LabelField("----------------------------------------------------------");
                        EditorGUILayout.BeginHorizontal();
                    }
                    SheetData.Ch3[l] = EditorGUILayout.Popup(SheetData.Ch3[l], Nodetype);
                    if (l == (SectionNo * 8) - 1)
                        EditorGUILayout.EndHorizontal();
                }

            }

            if (GUILayout.Button("存檔", GUILayout.Width(100)))
            {
                SheetDataManager.Save(SheetData, index);
            }

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




}
