using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItemObject : MonoBehaviour
{
    int maxItemQuantity = 50;
    int minItemQuantity = 20;
    int typeItemCount;
    int averageQuantity;
    int totalQuantity;
    int goldQuantity;
    int rockQuantity;
    int diamondQuatity;
    int bagQuantity;
    int mouseQuantity;
    int mouseDiamondQuantity;
    int tntQuantity;
    float totalValue;
    // position limit
    float limitX = 9.5f;
    float minY = -4.5f;
    float maxY = 0;
    // ItemObject
    public ArrayList itemObject;

    // level maybe appear item object
    int levelMaybeAppearRock = 2;
    int levelMaybeAppearDiamond = 4;
    int levelMaybeAppearBag = 3;
    int levelMaybeAppearMouse = 5;
    int levelMaybeAppearTNT = 7;

    // use item
    public bool useFourLeafClover = false;

    private void Awake()
    {
        itemObject = new ArrayList();
        totalQuantity = Random.Range(minItemQuantity, maxItemQuantity);
        typeItemCount = 3;
        averageQuantity = totalQuantity / typeItemCount;

        // Test();

        CreateGold();
        CreateRock();
        CreateDiamond();
        createBag();
        createTNT();
        createMouse();
    }

    // GOLD
    void CreateGold()
    {
        int goldMinQuantity = 5;
        goldQuantity = Random.Range(goldMinQuantity, averageQuantity);
        if (goldMinQuantity > averageQuantity)
        {
            goldQuantity = goldMinQuantity;
        }
        for (int i = 0; i < goldQuantity; i++)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/Gold"));
            item.transform.position = GetVector3();
            itemObject.Add(item);
            totalValue += item.GetComponent<Gold>().GetValue();
        }
    }

    // ROCK
    void CreateRock()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level < levelMaybeAppearRock)
        {
            return;
        }
        rockQuantity = Random.Range(0, averageQuantity);
        for (int i = 0; i < rockQuantity; i++)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/Rock"));
            item.transform.position = GetVector3();
            itemObject.Add(item);
            totalValue += item.GetComponent<Rock>().GetValue();
        }
    }

    // DIAMOND
    void CreateDiamond()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level < levelMaybeAppearDiamond)
        {
            return;
        }
        if (averageQuantity > 8)
        {
            diamondQuatity = Random.Range(0, 8);
        }
        else
        {
            diamondQuatity = Random.Range(0, averageQuantity);
        }
        for (int i = 0; i < diamondQuatity; i++)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/Diamond"));
            item.transform.position = GetVector3();
            itemObject.Add(item);
            totalValue += item.GetComponent<Diamond>().GetValue();
        }
    }

    void createBag()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level < levelMaybeAppearBag)
        {
            return;
        }
        bagQuantity = Random.Range(0, 5);
        for (int i = 0; i < bagQuantity; i++)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/Bag"));
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().useFourLeafClover)
            {
                item.GetComponent<Bag>().setupTypeValue();
            }
            item.transform.position = GetVector3();
            itemObject.Add(item);
        }
    }

    void createTNT()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level < levelMaybeAppearTNT)
        {
            return;
        }
        tntQuantity = Random.Range(0, averageQuantity);
        for (int i = 0; i < tntQuantity; i++)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/TNT"));
            item.transform.position = GetVector3();
            itemObject.Add(item);
            totalValue += item.GetComponent<TNT>().GetValue();
        }
    }

    void createMouse()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level < levelMaybeAppearMouse)
        {
            return;
        }
        mouseQuantity = Random.Range(0, averageQuantity 
        + GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level / 10);
        for (int i = 0; i < mouseQuantity; i++)
        {
            Debug.Log("CREATE MOUSE");
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/Mouse"));
            item.transform.position = GetVector3();
            itemObject.Add(item);
            totalValue += item.GetComponent<Mouse>().GetValue();
        }
    }



    // random position
    Vector3 GetVector3()
    {
        float x = (Random.Range(0, 100) % 2 == 0 ? -1 : 1) * Random.Range(0, limitX);
        float y = Random.Range(0, maxY - minY) + minY;
        return new Vector3(x, y, 0);
    }

    public float GetTotal()
    {
        return totalValue;
    }

    public void DestroyAll()
    {
        foreach (GameObject go in itemObject)
        {
            Destroy(go);
        }
    }

    void Test()
    {
        levelMaybeAppearBag = 0;
        levelMaybeAppearDiamond = 0;
        levelMaybeAppearMouse = 0;
        levelMaybeAppearRock = 0;
        levelMaybeAppearTNT = 0;
    }
}
