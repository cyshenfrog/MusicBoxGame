using UnityEngine;
using System.Collections.Generic;

public class TEST : MonoBehaviour {
    static SheetData data;

    // Use this for initialization
    void Start ()
    {
        data = new SheetData();
        for (int i = 0; i < 5; i++)
        {
            data.Add(new Section());
        }
        SheetDataManager.Save(data);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
