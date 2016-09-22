using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;

public static class EasyFunctions {
    //I don't know how the following 3 functions work, but they work well
    //Draw a number of int between min and max(inclusive)
    public static int[] DrawFrom(int min, int max, int number)
    {
        return Enumerable.Range(min, max-min+1)
            .OrderBy(a => Guid.NewGuid())
            .Take(number).OrderBy(p => p)
            .ToArray();
    }
    //Order the array ramdomly 
    public static string[] ReadCsvLine(string csv)//this function can't read the element include ','
    {
        return csv.Split(',');
    }
    //how many int which is target in sample 
    public static int CalculateExistTimes(int[] sample, int target)
    {
        int n = 0;
        for (int i = 0; i < sample.Length; i++)
        {
            if (sample[i] == target)
            {
                n++;
            }
        }
        return n;
    }
    //times: the correct times
    public static int CalculateAbilityIncrease(int times)
    {
        int result = 0;
        for(int i=0;i<times;i++)
        {
            result += UnityEngine.Random.Range(2, 6);
        }
        return result;
    }


    public static void Save(object obj, string path, Type type)
    {
        CreateXML(SerializeObject(obj, type), path);
    }
    public static object Load(object defaultObj, string path, Type type)
    {
        string _data;
        _data = LoadXML(path);
        //        Debug.Log (_data);
        if (_data == null)
        {
            return defaultObj;

        }
        return (DeserializeObject(_data, type, defaultObj));
    }

    //----序列化
    private static string SerializeObject(object pObject, Type type)
    {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(type);//需要更改類別
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }
    public static object DeserializeObject(string pXmlizedString, Type type, object defaultObj)
    {
        XmlSerializer xs;
        MemoryStream memoryStream;
        object o;
        try
        {
            xs = new XmlSerializer(type);//需要更改類別
            memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            o = xs.Deserialize(memoryStream);
        }
        catch { return defaultObj; }
        // XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return o;
    }

    //-----------------------------基礎內部function
    private static string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }
    private static byte[] StringToUTF8ByteArray(string pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }
    private static void CreateXML(string data, string fileLocation)
    {
        StreamWriter writer;
        FileInfo t = new FileInfo(fileLocation);
        if (!t.Exists)
        {
            writer = t.CreateText();
        }
        else
        {
            t.Delete();
            writer = t.CreateText();
        }
        writer.Write(data);
        writer.Close();

    }
    private static string LoadXML(string fileLocation)
    {
        if (File.Exists(fileLocation))
        {

            StreamReader r = File.OpenText(fileLocation);
            string _info = r.ReadToEnd();
            r.Close();

            return _info;
        }
        //        Debug.Log("file not found");
        return null;
    }
}


