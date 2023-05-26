using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    ItemObject itemObject;
    float percent;

    private void Awake() {
        itemObject = new ItemObject();
        itemObject.size = GameDefine.minSize;
        itemObject.value = GameDefine.diamondValue;
        itemObject.weight = GameDefine.diamondWeight;

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
