using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;
//using System.Web.Script.Serialization;
using UnityEngine;

public static class SheetDataManager  {
    static string directoryPath = Application.dataPath + @"\Saving";
    static string saveDataPath = Application.dataPath + @"\Saving\SaveData.xml";
    
    //static SheetData saveData = Load();


    public static void Save(SheetData saveData)
    {
        //JavaScriptSerializer objse = new JavaScriptSerializer();
        var serializer = new XmlSerializer(typeof(SheetData));

        //to ensure the encoding
        using (var stream = new StreamWriter(saveDataPath, false, Encoding.UTF8))
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            serializer.Serialize(stream, saveData);
        }
    }
    public static void Save(SheetData[] saveData)
    {
        var serializer = new XmlSerializer(typeof(SheetData));

        //to ensure the encoding
        using (var stream = new StreamWriter(saveDataPath, false, Encoding.UTF8))
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            serializer.Serialize(stream, saveData);
        }
    }

    public static SheetData Load()
    {

        if (File.Exists(saveDataPath))
        {
            //to read the saveData
            var serializer = new XmlSerializer(typeof(GameData));

            using (var stream = new FileStream(saveDataPath, FileMode.Open))
            {
                return (SheetData)serializer.Deserialize(stream);
            }
        }
        else
        {
            Debug.LogError("The creation of directoryPath is fail");
            return null;
        }
    }
    public static List<SheetData> LoadList()
    {

        if (File.Exists(saveDataPath))
        {
            //to read the saveData
            var serializer = new XmlSerializer(typeof(List<SheetData>));

            using (var stream = new FileStream(saveDataPath, FileMode.Open))
            {
                return (List<SheetData>)serializer.Deserialize(stream);
            }
        }
        else
        {
            Debug.LogError("The creation of directoryPath is fail");
            return null;
        }
    }

}
