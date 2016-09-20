using UnityEngine;
using System.Collections;

public class ResultDataManager : MonoBehaviour {
    public static ResultDataManager instance;
    int currentCombo;
    int maxCombo;
    int totalMiss;
    int score;
    int totalNodeCount;
    bool isComboing;
    public UISprite[] Rank = new UISprite[6];
    UISprite rankResult;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            Debug.LogError("2 ResultDataManager detected");
        }


    }

    public void ComboJudge(bool isCombo)
    {
        if (isCombo)
        {
            currentCombo++;
            if (currentCombo > maxCombo)
                maxCombo = currentCombo;
            if (!isComboing)
                isComboing = true;
            Debug.Log("combo" + currentCombo);
        }
        else
        {
            currentCombo = 0;
            totalMiss++;
            if (isComboing)
                isComboing = false;
            Debug.Log("miss!!");

        }
    }

    public void CalculateResult(int TotalNode)
    {
        if (TotalNode != totalNodeCount)
        {
            Debug.LogError("TotalNode != totalNodeCount!! Missing Judge?");
            totalNodeCount = TotalNode;
        }
        float rate = totalMiss / totalNodeCount;
        if (rate == 0)
            rankResult = Rank[0];
        else if(rate > 0.05)
            rankResult = Rank[1];
        else if (rate > 0.1)
            rankResult = Rank[2];
        else if (rate > 0.15)
            rankResult = Rank[3];
        else if (rate > 0.2)
            rankResult = Rank[4];
        else
            rankResult = Rank[5];
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
