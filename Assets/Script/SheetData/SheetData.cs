using System.Collections.Generic;
using System.Collections;
using System;


public class SheetData   {
    public int Bpm;
    public string Audio;
    public string[] Nodetype = new string[4] { "  ", "Red", "Green", "Blue" };
    public List<int> Ch1 = new List<int>();
    public List<int> Ch2 = new List<int>();
    public List<int> Ch3 = new List<int>();

}
