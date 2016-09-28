using System.Collections.Generic;
using System.Collections;
using System;

[Serializable]
public class SheetData   {
    public int Bpm;
    public string Audio;
    public List<Section> section = new List<Section>();
    public string[] Nodetype = new string[4] { "  ", "Red", "Green", "Blue" };
    public List<int> Beat = new List<int>();

}
