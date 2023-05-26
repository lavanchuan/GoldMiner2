using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    float price = 2;
    float speed;
    float lastSpeed;
    bool isMove = false;
    public bool isDiamond = false;
    bool rightDirect = false;

    // level can action
    int levelMaybeMove = 8;
    int levalMaybeDiamond;

    // MainCamera
    GameObject MainCamera;
    int levelPlayer;

    // ANIMATOR
    Animator ani;

    private void Awake() {
        // constant
        levalMaybeDiamond = levelMaybeMove + 2;

        /// <summary>
        /// TETS
        /// </summary>
        // levelMaybeMove = 0;
        // levalMaybeDiamond = 0;

        // MainCamera
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        levelPlayer = MainCamera.GetComponent<Player>().level;

        // create
        if(levelPlayer + 1 >= levelMaybeMove && ((int)Random.Range(1, 1000)) % 2 == 0) isMove = true;
        if(levelPlayer + 1 >= levalMaybeDiamond && ((int)Random.Range(1, 1000)) % 2 == 0) isDiamond = true;
        if(isMove) speed = Random.Range(0.1f, 0.4f);
        if(isDiamond) price += 800;
        if(speed != 0 && ((int)Random.Range(1, 1000)) % 2 == 0) rightDirect = true;
    }

    private void Start() {
        ani = GetComponent<Animator>();

        lastSpeed = speed;
    }

    private void Update() {

        ani.SetBool("IsMove", isMove);
        ani.SetBool("IsDiamond", isDiamond);
        if(speed != 0) Move();

        if(GameObject.FindGameObjectWithTag("BtnPause").GetComponent<BtnPause>().isPause){
            speed = 0;
        } else {
            speed = lastSpeed;
        }
    }

    void Move(){
        Debug.Log("Moving");
        if(rightDirect && transform.localScale.x > 0
        || !rightDirect && transform.localScale.x < 0){
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        }
        if(rightDirect && speed < 0
        || !rightDirect && speed > 0) speed *= -1;
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Hook"){
            speed = 0;
        } else if(other.contacts[0].normal.x != 0){
            rightDirect = !rightDirect;
        }
    }

    public float GetValue(){
        return this.price;
    }
}
