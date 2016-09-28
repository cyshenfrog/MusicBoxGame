using System.Collections.Generic;
using System.Collections;
using System;

public class SheetData : ICollection  {
    public int Bpm;
    List<Section> sheetList = new List<Section>();
    public string Audio;
    public List<Section> section;
    public Section this[int index]
    {
        get { return (Section)sheetList[index]; }
    }
    public void CopyTo(Array a, int index)
    {
        sheetList.ToArray().CopyTo(a, index);
    }
    public int Count
    {
        get { return sheetList.Count; }
    }
    public object SyncRoot
    {
        get { return this; }
    }
    public bool IsSynchronized
    {
        get { return false; }
    }
    public IEnumerator GetEnumerator()
    {
        return sheetList.GetEnumerator();
    }
    public void Add(Section newEmployee)
    {
        sheetList.Add(newEmployee);
    }

}
