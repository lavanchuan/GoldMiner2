using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour 
{
    ItemObject itemObject;
    float percent;

    private void Awake() {
        itemObject = new ItemObject();
        itemObject.size = Random.Range(GameDefine.minSize, GameDefine.maxSize);
        percent = itemObject.size / GameDefine.minSize;
        itemObject.value = GameDefine.goldValue * percent;
        itemObject.weight = GameDefine.goldWeight * percent;

        // init
        transform.localScale = new Vector3(itemObject.size, itemObject.size, 0);
    }

    public float GetSize(){
        return itemObject.size;
    }

    public float GetValue(){
        return itemObject.value;
    }

    public float GetWeight(){
        return itemObject.weight;
    }
}
