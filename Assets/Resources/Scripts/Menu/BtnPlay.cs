using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPlay : MonoBehaviour
{
    string textBtn = "PLAY";
    string textLevel = "LEVEL: ";
    string textMoney = "$: ";
    int level;
    float money = 2000;
    public string descript = "";
    public float price;
    public string TagItemBuy = "";

    // use item function
    public float strength;
    public float xValueDiamond;
    public bool onX3Value;
    public int timeAdd;

    private void Update()
    {
        level = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level;
        money = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().money;
    }

    private void OnGUI()
    {

        float screenWidth = Screen.width / 2f;
        float screenHeight = Screen.height / 2f;
        float width = screenWidth / 2;
        float height = screenHeight / 4;
        float x;
        float y;
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));

        // button
        style.fontSize = (int)screenHeight / 4;
        x = screenWidth - width / 2;
        y = screenHeight - height / 2;

        if (GUI.Button(new Rect(x, y, width, height), textBtn, style))
        {
            StartCoroutine(AniTranslate());
        }

        // label level
        style.fontSize = (int)screenHeight / 5;
        float margin = screenWidth / 10;
        x = margin;
        y = screenHeight - height / 2;
        float w = width + margin;
        float h = height;

        GUI.Label(new Rect(x, y, w, h), textLevel + level, style);

        // label money
        x = 2 * screenWidth - width - 2 * margin;

        GUI.Label(new Rect(x, y, w, h), textMoney + (int)money, style);

        // area descript
        if (!descript.Equals(""))
        {
            style.fontSize = (int)screenHeight / 10;
            x = 3 * margin;
            y += 1.5f * margin;
            w = 2 * screenWidth - 4 * margin;
            height = screenHeight / 10;
            GUI.Label(new Rect(x + 10, y, w - 10, h), descript, style);

            // btn buy
            x -= 2 * margin;
            w = 2 * margin - 10;

            if (price > 0 && GUI.Button(new Rect(x, y, w, h), "$" + (int)price, style))
            {
                BuyItem();
            }
        }

    }

    void BuyItem()
    {
        if (money < price)
        {
            descript = "Bạn không đủ tiền để mua vật phẩm này :(";
        }
        else
        {
            money -= price;
            price = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
            .PlayAudio(AudioMute.buy_sound);
            // update
            GameObject curentItem = GameObject.FindGameObjectWithTag(TagItemBuy);
            Destroy(curentItem);
            descript = "Mua vật phẩm thành công";

            PlayerUpdate();
        }
    }

    void PlayerUpdate()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level = level;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().money = money;

        switch (TagItemBuy)
        {
            case "Bottle1":
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().strength = strength;
                break;
            case "Bottle2":
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().xValueDiamond = xValueDiamond;
                break;
            case "BottleXXX":
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().onX3Value = onX3Value;
                break;
            case "Clock":
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().timeAdd = timeAdd;
                break;
            case "FireCracker":
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().fireCreackerQuantity++;
                break;
            case "FourLeafClover":
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().useFourLeafClover = true;
                break;
        }
    }


    IEnumerator AniTranslate()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Resources.Load("Prefabs/Play"));
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().playing = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>()
        .SetTimeSecond(Player.TIME_DEFAULT);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().gameScreen
        = GameDefine.GameScreen.PLAY;
        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        Destroy(Menu);
    }


}
