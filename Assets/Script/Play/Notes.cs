using UnityEngine;
using System.Collections;

public class Notes : MonoBehaviour {
    //SheetMusicData MySheetMusic;//需要反序列化來初始他的值
    public int bpm;
    public float Delay;
    public int NodeType;
    float speedmultiplier;
    public UISprite Sprite;
    TweenPosition TP;
    TweenAlpha TA;
    TweenScale TS;


    //public int Bpm
    //{
    //    set
    //    {
    //        bpm = value;
    //    }
    //    get
    //    {
    //        return bpm;
    //    }
    //}

    //public int Speedmultiplier
    //{
    //    set
    //    {
    //        Speedmultiplier = value;
    //    }
    //}
	
	// slot range is 1500 , 1x speed have 8 beat
	void Start ()
    {
        if (NodeType == 0)
            Sprite.spriteName = null;
        else if (NodeType >0&& NodeType<4)
            Sprite.spriteName = NodeType.ToString();
        else
            Debug.LogError("wrong color code");
        speedmultiplier = SettingData.Speed;
        TA = gameObject.GetComponent<TweenAlpha>();
        TP = gameObject.GetComponent<TweenPosition>();
        TS = gameObject.GetComponent<TweenScale>();
        TP.delay = Delay;
        TP.SetStartToCurrentValue();
        TP.to =  Vector3.left * 600;
        TP.duration = 8f / bpm * 60f / speedmultiplier;
        TP.PlayForward();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Instantiate FX here
        Debug.Log(other.tag);
        if (other.tag == gameObject.tag)
        {
            ResultDataManager.instance.ComboJudge(true);
            hitAnimate();
        }
        else
        {
            ResultDataManager.instance.ComboJudge(false);
            missAnimate();
        }
    }


    void missAnimate()
    {
        //有問題待完工
        Destroy(TP);
        TP = gameObject.AddComponent<TweenPosition>();
        TP.enabled = false;
        TP.SetStartToCurrentValue();
        TP.to = Vector3.left * 660 ;
        TP.duration = 1;
        TP.delay = 0;
        TP.enabled = true; 
        TA.enabled = true;
    }


    void hitAnimate()
    {
        Destroy(TP);
        TS.enabled = true;
        TA.enabled = true;
    }
}
