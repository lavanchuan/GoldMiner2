using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    ItemObject itemObject;

    string typeValue;
    string[] typeValues = {
    "FIRE_CRACKER",
    "DIAMOND",
    "MONEY",
    "TIME",
    "TONIC"};

    private void Awake()
    {
        itemObject = new ItemObject();
        itemObject.weight = GameDefine.bagWeight;

        typeValue = typeValues[(int)Random.RandomRange(0, typeValues.Length - 1)];
    }

    public void setupTypeValue()
    {
        typeValue = typeValues[(int)Random.RandomRange(0, 2)];
    }

    public float GetWeight()
    {
        return itemObject.weight;
    }

    public void GetBag()
    {
        switch (typeValue)
        {
            case "MONEY":
                float money = Random.Range(0, 1000);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().money += money;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>().OnMoneyAdd(money);
                break;
            case "FIRE_CRACKER":
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().fireCreackerQuantity++;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>().OnMessage("Pickup fire cracker");
                break;
            case "TONIC":
                float strength = Random.Range(1, 2);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().strength += strength;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>().OnMessage("Pickup energy drink");
                break;
            case "TIME":
                int time = Random.Range(0, 30);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().timeSecond += time;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>().OnMessage("Added time");
                break;
            case "DIAMOND":
                float mn = GameDefine.diamondValue;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().money += mn;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>().OnMoneyAdd(mn);
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>().OnMessage("Pickup diamond");
                break;
        }

        Debug.Log(typeValue);
    }
}
