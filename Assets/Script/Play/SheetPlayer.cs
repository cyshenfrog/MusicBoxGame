using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheetPlayer : MonoBehaviour {
    SheetData MySheetMusic;//需要反序列化來初始他的值
    public GameObject Channel_Start1;
    public GameObject Channel_Start2;
    public GameObject Channel_Start3;
    public UISprite GetReady;
    List<GameObject> Ch1_Node = new List<GameObject>();
    List<GameObject> Ch2_Node = new List<GameObject>();
    List<GameObject> Ch3_Node = new List<GameObject>();

    void Start () {
        //Instantiate(Resources.Load())
        //MySheetMusic.bpm = 120;
        //MySheetMusic.Channel_1 = new List<float>();
        //for (int i = 0; i < 1000; i++)
        //{
        //    MySheetMusic.Channel_1.Add(i);
        //}
        MySheetMusic = SheetDataManager.Load(0);
        Ready();
	
	}
	
	void Update () {
	
	}

    void Ready()
    {
        GetReady.GetComponent<UITweener>().PlayForward();
        StartCoroutine(GeneratNodes());
        Debug.Log("!!!");

    }

    public IEnumerator GeneratNodes()
    {
        //int Ch1_lenth = MySheetMusic.Channel_1.Capacity;
        //int Ch2_lenth = MySheetMusic.Channel_2.Capacity;
        //int Ch3_lenth = MySheetMusic.Channel_3.Capacity;
        ResourceRequest rq1;
        ResourceRequest rq2;
        ResourceRequest rq3;
        for (int i = 0; i < MySheetMusic.Ch1.Count; i++)
        {
            //if (MySheetMusic.Beat[i] != 0)
                rq1 = Resources.LoadAsync("node");
                Debug.Log(i);
                Ch1_Node.Add(GameObject.Instantiate((GameObject)rq1.asset));
                Ch1_Node[i].transform.parent = Channel_Start1.transform;
                Ch1_Node[i].transform.localPosition = Vector3.zero;
                Ch1_Node[i].transform.localScale = Vector3.one;
                Ch1_Node[i].GetComponent<Notes>().bpm = MySheetMusic.Bpm;
                Ch1_Node[i].GetComponent<Notes>().NodeType = MySheetMusic.Ch1[i];
                Ch1_Node[i].GetComponent<Notes>().Delay = i;
                Debug.Log("loaded ch1 " + i);
                yield return rq1.isDone;
        }
        for (int j = 0; j < MySheetMusic.Ch2.Count; j++)
        {
            //if (MySheetMusic.Beat[i] != 0)
            rq2 = Resources.LoadAsync("node");
            Debug.Log(j);
            Ch2_Node.Add(GameObject.Instantiate((GameObject)rq2.asset));
            Ch2_Node[j].transform.parent = Channel_Start2.transform;
            Ch2_Node[j].transform.localPosition = Vector3.zero;
            Ch2_Node[j].transform.localScale = Vector3.one;
            Ch2_Node[j].GetComponent<Notes>().bpm = MySheetMusic.Bpm;
            Ch2_Node[j].GetComponent<Notes>().NodeType = MySheetMusic.Ch2[j];
            Ch2_Node[j].GetComponent<Notes>().Delay = j;
            Debug.Log("loaded ch1 " + j);
            yield return rq2.isDone;
        }
        for (int k = 0; k < MySheetMusic.Ch3.Count; k++)
        {
            //if (MySheetMusic.Beat[i] != 0)
            rq3 = Resources.LoadAsync("node");
            Debug.Log(k);
            Ch3_Node.Add(GameObject.Instantiate((GameObject)rq3.asset));
            Ch3_Node[k].transform.parent = Channel_Start3.transform;
            Ch3_Node[k].transform.localPosition = Vector3.zero;
            Ch3_Node[k].transform.localScale = Vector3.one;
            Ch3_Node[k].GetComponent<Notes>().bpm = MySheetMusic.Bpm;
            Ch3_Node[k].GetComponent<Notes>().NodeType = MySheetMusic.Ch3[k];
            Ch3_Node[k].GetComponent<Notes>().Delay = k;
            Debug.Log("loaded ch1 " + k);
            yield return rq3.isDone;
        }

        Debug.Log("YO");
        if (GetReady.GetComponent<UITweener>().enabled)
            GetReady.GetComponent<UITweener>().onFinished.Add(new EventDelegate(() =>
            {
                GetReady.GetComponent<UITweener>().PlayReverse();
            }));
        else
            GetReady.GetComponent<UITweener>().PlayReverse();
        if (GetReady.GetComponent<UITweener>().isforward)
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return null;
                if (!GetReady.GetComponent<UITweener>().isforward)
                    break;

            }

        }
        Debug.Log("YOYO");
        GetReady.GetComponent<UITweener>().onFinished.Add(new EventDelegate(Play));

    }

    public void Play()
    {
        for (int i = 0; i < MySheetMusic.Ch1.Count; i++)
        {
            Ch1_Node[i].SetActive(true);
        }
        for (int i = 0; i < MySheetMusic.Ch2.Count; i++)
        {
            Ch2_Node[i].SetActive(true);
        }
        for (int i = 0; i < MySheetMusic.Ch3.Count; i++)
        {
            Ch3_Node[i].SetActive(true);
        }

        Debug.Log("GO!");
    }
}
