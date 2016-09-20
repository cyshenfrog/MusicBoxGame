using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheetPlayer : MonoBehaviour {
    SheetMusicData MySheetMusic;//需要反序列化來初始他的值
    public GameObject Channel_Start1;
    public GameObject Channel_Start2;
    public GameObject Channel_Start3;
    public UISprite GetReady;
    List<GameObject> Ch1_Node = new List<GameObject>();
    List<GameObject> Ch2_Node = new List<GameObject>();
    List<GameObject> Ch3_Node = new List<GameObject>();

    void Start () {
        //Instantiate(Resources.Load())
        MySheetMusic.bpm = 120;
        MySheetMusic.Channel_1 = new List<float>();
        for (int i = 0; i < 1000; i++)
        {
            MySheetMusic.Channel_1.Add(i);
        }
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
        ResourceRequest rq;
        for (int i = 0; i < 1000; i++)
        {
            rq = Resources.LoadAsync("node");
            Debug.Log(i);
            Ch1_Node.Add(GameObject.Instantiate((GameObject)rq.asset));
            Ch1_Node[i].transform.parent = Channel_Start1.transform;
            Ch1_Node[i].transform.localPosition = Vector3.zero;
            Ch1_Node[i].transform.localScale = Vector3.one;
            Ch1_Node[i].GetComponent<Notes>().bpm = MySheetMusic.bpm;
            Ch1_Node[i].GetComponent<Notes>().Delay = MySheetMusic.Channel_1[i];
            Debug.Log("loaded ch1 " + i);
            yield return rq.isDone;
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
                //Debug.Log(i);
                if (!GetReady.GetComponent<UITweener>().isforward)
                    break;
            }
        }
        Debug.Log("YOYO");
        GetReady.GetComponent<UITweener>().onFinished.Add(new EventDelegate(Play));

    }

    public void Play()
    {
        for (int i = 0; i < 1000; i++)
        {
            Ch1_Node[i].SetActive(true);
        }

        Debug.Log("GO!");
    }
}
