using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    float timeShowMoneyAdd = 1f;
    bool showMoneyAdd = false;
    float moneyAdd = 800;
    string text;
    bool onText = false;

    void OnGUI()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        int x, y, w, h;
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = screenHeight / 20;
        // Money Add
        w = screenWidth / 5;
        h = screenHeight / 15;
        x = screenWidth / 2 - w / 2 - 200;
        y = 2 * screenHeight / 10;
        if (showMoneyAdd)
        {
            GUI.Label(new Rect(x, y, w, h), "+" + (int)moneyAdd + "$", style);
            StartCoroutine(AniWaitTimeMoney());
        }
        if (onText)
        {
            GUI.Label(new Rect(x - (3 * w / 2), y + h, 3 * w, h), text, style);
            StartCoroutine(AniWaitTimeText());
        }
    }

    public void OnMoneyAdd(float money)
    {
        this.moneyAdd = money;
        showMoneyAdd = true;
    }

    public void OnMessage(string msg)
    {
        this.text = msg;
        onText = true;
    }

    IEnumerator AniWaitTimeText()
    {
        yield return new WaitForSeconds(timeShowMoneyAdd);
        onText = false;
    }

    IEnumerator AniWaitTimeMoney()
    {
        yield return new WaitForSeconds(timeShowMoneyAdd);
        showMoneyAdd = false;
    }
}
