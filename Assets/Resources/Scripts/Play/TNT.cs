using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public bool normal = true;
    Animator ani;
    ItemObject itemObject;
    bool coled = false;

    private void Awake()
    {
        itemObject = new ItemObject();
        itemObject.value = GameDefine.tntValue;
        itemObject.weight = GameDefine.tntWeight;
    }

    public float GetValue()
    {
        return itemObject.value;
    }

    public float GetWeight()
    {
        return itemObject.weight;
    }

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("Normal", normal);
        Debug.Log(coled);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Hook" && !coled){
            normal = false;
            coled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            GameObject explode = (GameObject) Instantiate(Resources.Load("Prefabs/ExplodeTNT"));
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
            .PlayAudio(AudioMute.tnt_sound);
            explode.transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
        };
    }

}
