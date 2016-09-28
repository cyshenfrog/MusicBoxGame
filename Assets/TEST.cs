using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class TEST : MonoBehaviour {
    static SheetData data;

    public string test;
    public int bpmTest;
    public List<int> beatTest;

    // Use this for initialization
    void Start ()
    {
        data = new SheetData();
        Debug.Log(data.Audio);
        Debug.Log(data.Bpm);

        Debug.Log(data.Beat);

        data.Audio = "a";
        data.Bpm = 120;
        for (int i = 0; i < 5; i++)
        {
            data.Beat.Add(i);
        }

        string json = JsonUtility.ToJson(data);
        string savePath = Application.dataPath + "/Resources/Test01.json";
        File.WriteAllText(savePath, json, Encoding.UTF8);

        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/Test01.json");
        string newjson = sr.ReadToEnd();
        SheetData newSheet = JsonUtility.FromJson<SheetData>(newjson);

        test = newSheet.Audio;
        bpmTest = newSheet.Bpm;
        for (int i = 0; i < 5; i++)
        {
            beatTest.Add(newSheet.Beat[i]);
        }

        Debug.Log(test);
        Debug.Log(bpmTest);

        Debug.Log(bpmTest);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
