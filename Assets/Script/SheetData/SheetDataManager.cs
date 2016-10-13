using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using UnityEngine;

public static class SheetDataManager  {
    //static string directoryPath = Application.dataPath + @"\Saving";
    static string saveDataPath = Application.dataPath + @"\Saving";
    
    //static SheetData saveData = Load();


    public static void Save(SheetData saveData, int SheetNo)
    {

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveDataPath + @"\Sheet" + SheetNo.ToString(), json, Encoding.UTF8);

    }
    public static void Save(EditorData saveData)
    {

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveDataPath + @"\EditorData" , json, Encoding.UTF8);

    }

    //public static void Save(SheetData[] saveData, int SheetNo)
    //{
    //    var serializer = new XmlSerializer(typeof(SheetData));

    //    //to ensure the encoding
    //    using (var stream = new StreamWriter(saveDataPath+@"\Sheet" + SheetNo.ToString(), false, Encoding.UTF8))
    //    {
    //        if (!Directory.Exists(directoryPath))
    //            Directory.CreateDirectory(directoryPath);
    //        serializer.Serialize(stream, saveData);
    //    }
    //}

    public static SheetData Load(int SheetNo)
    {

        if (File.Exists(saveDataPath + @"\Sheet" + SheetNo.ToString()))
        {
            using (StreamReader sr = new StreamReader(saveDataPath + @"\Sheet" + SheetNo.ToString()))
            {
                string newjson = sr.ReadToEnd();
                SheetData newData = JsonUtility.FromJson<SheetData>(newjson);
                return newData;

            }
        }
        else
        {
            Debug.LogError("The creation of directoryPath is fail");
            return null;
        }
    }

    public static EditorData Load()
    {

        if (File.Exists(saveDataPath + @"\EditorData"))
        {
            using (StreamReader sr = new StreamReader(saveDataPath + @"\EditorData"))
            {
                string newjson = sr.ReadToEnd();
                EditorData newData = JsonUtility.FromJson<EditorData>(newjson);
                return newData;

            }
        }
        else
        {
            Debug.LogError("The creation of directoryPath is fail");
            return null;
        }
    }


    //public static List<SheetData> LoadList()
    //{

    //    if (File.Exists(saveDataPath))
    //    {
    //        //to read the saveData
    //        var serializer = new XmlSerializer(typeof(List<SheetData>));

    //        using (var stream = new FileStream(saveDataPath, FileMode.Open))
    //        {
    //            return (List<SheetData>)serializer.Deserialize(stream);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("The creation of directoryPath is fail");
    //        return null;
    //    }
    //}

    public static bool isSavedataExist(int no)
    {
        return File.Exists(saveDataPath + @"\Sheet" + no.ToString());
    }

    public static bool isEditorDataExist()
    {
        return File.Exists(saveDataPath + @"\EditorData");
    }

}
