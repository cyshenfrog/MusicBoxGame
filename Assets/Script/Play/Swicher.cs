using UnityEngine;
using System.Collections;

public class Swicher : MonoBehaviour {

    public UISprite Channel1;
    public UISprite Channel2;
    public UISprite Channel3;
    int Channel1SpriteNumber;
    int Channel2SpriteNumber;
    int Channel3SpriteNumber;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            if (gameObject.transform.localPosition.y < 199)
                gameObject.transform.localPosition += Vector3.up * 200;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            if (gameObject.transform.localPosition.y > -199)
                gameObject.transform.localPosition -= Vector3.up * 200;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gameObject.transform.localPosition.y == 200)
            {
                Channel1SpriteNumber++;
                if (Channel1SpriteNumber == 3)
                    Channel1SpriteNumber = 0;
                ChangeColor(Channel1, Channel1SpriteNumber.ToString());
            }
            if (gameObject.transform.localPosition.y == 0)
            {
                Channel2SpriteNumber++;
                if (Channel2SpriteNumber == 3)
                    Channel2SpriteNumber = 0;
                ChangeColor(Channel2, Channel2SpriteNumber.ToString());
            }
            if (gameObject.transform.localPosition.y == -200)
            {
                Channel3SpriteNumber++;
                if (Channel3SpriteNumber == 3)
                    Channel3SpriteNumber = 0;
                ChangeColor(Channel3, Channel3SpriteNumber.ToString());
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gameObject.transform.localPosition.y == 200)
            {
                Channel1SpriteNumber--;
                if (Channel1SpriteNumber == -1)
                    Channel1SpriteNumber = 2;
                ChangeColor(Channel1, Channel1SpriteNumber.ToString());
            }
            if (gameObject.transform.localPosition.y == 0)
            {
                Channel2SpriteNumber--;
                if (Channel2SpriteNumber == -1)
                    Channel2SpriteNumber = 2;
                ChangeColor(Channel2, Channel2SpriteNumber.ToString());
            }
            if (gameObject.transform.localPosition.y == -200)
            {
                Channel3SpriteNumber--;
                if (Channel3SpriteNumber ==-1)
                    Channel3SpriteNumber = 2;
                ChangeColor(Channel3, Channel3SpriteNumber.ToString());
            }
        }
    }

        void ChangeColor(UISprite channelSprite, string number)
    {
        channelSprite.spriteName = number;
        channelSprite.tag = number;
    }
}
