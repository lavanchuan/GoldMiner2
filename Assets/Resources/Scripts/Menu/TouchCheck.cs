using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    GameObject Item;
    GameObject Menu;
    string descript;
    float price;

    GameObject CameraGO;

    private void Awake()
    {
        CameraGO = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CameraGO.GetComponent<GameManager>().gameScreen == GameDefine.GameScreen.MENU)
        {
            Item = GameObject.FindGameObjectWithTag(other.collider.tag);
            Menu = GameObject.FindGameObjectWithTag("Menu");
            bool update = true;
            switch (Item.tag)
            {
                case "FireCracker":
                    descript = Item.GetComponent<FireCreacker>().Descript;
                    price = Item.GetComponent<FireCreacker>().Price;
                    break;
                case "FourLeafClover":
                    descript = Item.GetComponent<FourLeafClover>().Descript;
                    price = Item.GetComponent<FourLeafClover>().Price;
                    break;
                case "Bottle1":
                    descript = Item.GetComponent<Bottle1>().Descript;
                    price = Item.GetComponent<Bottle1>().Price;
                    Menu.GetComponent<BtnPlay>().strength = Item.GetComponent<Bottle1>().strength;
                    break;
                case "Bottle2":
                    descript = Item.GetComponent<Bottle2>().Descript;
                    price = Item.GetComponent<Bottle2>().Price;
                    Menu.GetComponent<BtnPlay>().xValueDiamond = Item.GetComponent<Bottle2>().GetXValue();
                    break;
                case "BottleXXX":
                    descript = Item.GetComponent<BottleXXX>().Descript;
                    price = Item.GetComponent<BottleXXX>().Price;
                    Menu.GetComponent<BtnPlay>().onX3Value = true;
                    break;
                case "Clock":
                    descript = Item.GetComponent<Clock>().Descript;
                    price = Item.GetComponent<Clock>().Price;
                    Menu.GetComponent<BtnPlay>().timeAdd = Item.GetComponent<Clock>().GetTimeAdd();

                    break;
                default:
                    update = false;
                    break;
            }

            if (update)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.touch_sound);

                Menu.GetComponent<BtnPlay>().descript = descript;
                Menu.GetComponent<BtnPlay>().price = price;
                Menu.GetComponent<BtnPlay>().TagItemBuy = Item.tag;
            }
        }
        else if (CameraGO.GetComponent<GameManager>().gameScreen == GameDefine.GameScreen.PLAY)
        {

        }
        else
        {

        }

    }
}
