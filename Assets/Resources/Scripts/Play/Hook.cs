// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    GameObject playerPlay;
    LineRenderer lr;
    GameObject camera;
    // property
    float angle;
    float speedRotation;
    const float MAX_SPEED_ROTATION = 2f;
    const float MIN_SPEED_ROTATION = 0.5f;
    const float MAX_ANGLE = 80;
    float speedShoot = 4f;
    float speedRewind = -6f;
    const float SPEED_REWIND_DEFAULT = -6f;
    // float tract; // luc keo
    bool col = false;
    Vector3 firstPos;

    // REWIND
    public GameObject goRewind = null;
    public bool onX3Value = false;

    private void Awake()
    {
        playerPlay = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        goRewind = null;
        speedRotation = Random.Range(MIN_SPEED_ROTATION, MAX_SPEED_ROTATION);
        speedShoot = 4f;
        speedRewind = SPEED_REWIND_DEFAULT;
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlay>().transform.position);
        lr.SetPosition(1, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlay>().transform.position);
        firstPos = transform.position;

        //
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlay>().playState
        = GameDefine.PlayState.PLAY;
        playerPlay.GetComponent<PlayerPlay>().playerState = GameDefine.PlayerState.ROTATION;
    }

    private void Update()
    {

        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlay>().playState)
        {
            case GameDefine.PlayState.PLAY:
                PlayState();
                break;
            case GameDefine.PlayState.PAUSE:
                PauseState();
                break;
            case GameDefine.PlayState.LOSE:
                LoseState();
                break;
        }
    }

    void PlayState()
    {
        switch (playerPlay.GetComponent<PlayerPlay>().playerState)
        {
            case GameDefine.PlayerState.ROTATION:
                Rotation();
                if (Input.touchCount > 0
                && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().gameScreen
                == GameDefine.GameScreen.PLAY)
                {
                    playerPlay.GetComponent<PlayerPlay>().playerState = GameDefine.PlayerState.SHOOT;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                    .PlayAudio(AudioMute.shoot_sound);
                }
                break;
            case GameDefine.PlayerState.SHOOT:
                Shoot();
                if (col)
                {
                    playerPlay.GetComponent<PlayerPlay>().playerState = GameDefine.PlayerState.REWIND;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                    .PlayAudio(AudioMute.rewind_sound);
                    col = false;

                    if (goRewind.tag != "Limit")
                    {
                        switch (goRewind.tag)
                        {
                            case "Gold":
                                speedRewind = GetSpeedRewind(goRewind.gameObject.GetComponent<Gold>().GetWeight());
                                break;
                            case "Rock":
                                speedRewind = GetSpeedRewind(goRewind.gameObject.GetComponent<Rock>().GetWeight());
                                break;
                            case "Diamond":
                                speedRewind = GetSpeedRewind(goRewind.gameObject.GetComponent<Diamond>().GetWeight());
                                break;
                        }
                    }
                }
                break;
            case GameDefine.PlayerState.REWIND:
                Rewind();
                if (transform.position.y > firstPos.y)
                {
                    transform.position = firstPos;
                    speedRewind = SPEED_REWIND_DEFAULT;
                    if (goRewind != null && goRewind.tag != "Limit")
                    {
                        UpdateRewind(goRewind);
                        Destroy(goRewind);
                    }
                    playerPlay.GetComponent<PlayerPlay>().playerState = GameDefine.PlayerState.ROTATION;
                }
                break;
        }
    }

    void PauseState()
    {
        Debug.Log("PAUSE");
    }

    void LoseState()
    {
        Debug.Log("LOSE");
    }

    void Rotation()
    {
        if (angle >= 80 || angle <= -80)
        {
            speedRotation *= -1;
        }
        angle += speedRotation;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }

    void Shoot()
    {
        float strength = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().strength;
        lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        transform.Translate(Vector3.down * speedShoot * strength * Time.deltaTime);
    }

    void Rewind()
    {
        float strength = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().strength;
        lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        transform.Translate(Vector3.down * speedRewind * strength * Time.deltaTime);

        if (goRewind != null && goRewind.tag != "Limit")
        {
            RewindObject(goRewind);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Touch") return;

        col = true;

        goRewind = other.gameObject;
    }

    void UpdateRewind(GameObject go)
    {
        float moneyAdd;
        onX3Value = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().onX3Value;
        switch (go.tag)
        {
            case "Gold":
                if (onX3Value)
                {
                    moneyAdd = 3 * go.gameObject.GetComponent<Gold>().GetValue();
                }
                else
                {
                    moneyAdd = go.gameObject.GetComponent<Gold>().GetValue();
                }
                camera.GetComponent<Player>().money += moneyAdd;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>()
                .OnMoneyAdd(moneyAdd);
                GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().itemObject.Remove(go.gameObject);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.pickup_sound);
                break;
            case "Rock":
                if (onX3Value)
                {
                    moneyAdd = 3 * go.gameObject.GetComponent<Rock>().GetValue();
                }
                else
                {
                    moneyAdd = go.gameObject.GetComponent<Rock>().GetValue();
                }
                camera.GetComponent<Player>().money += moneyAdd;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>()
                .OnMoneyAdd(moneyAdd);
                GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().itemObject.Remove(go.gameObject);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.pickup_sound);
                break;
            case "Diamond":
                if (!onX3Value)
                {
                    moneyAdd = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().xValueDiamond * go.gameObject.GetComponent<Diamond>().GetValue();
                }
                else
                {
                    moneyAdd = 3 * go.gameObject.GetComponent<Diamond>().GetValue();
                }
                camera.GetComponent<Player>().money += moneyAdd;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>()
                .OnMoneyAdd(moneyAdd);
                GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().itemObject.Remove(go.gameObject);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.diamond_sound);
                break;
            case "TNT":
                if (onX3Value)
                {
                    moneyAdd = 3 * go.gameObject.GetComponent<TNT>().GetValue();
                }
                else
                {
                    moneyAdd = go.gameObject.GetComponent<TNT>().GetValue();
                }
                camera.GetComponent<Player>().money += moneyAdd;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>()
                .OnMoneyAdd(moneyAdd);
                GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().itemObject.Remove(go.gameObject);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.pickup_sound);
                break;
            case "Bag":
                go.GetComponent<Bag>().GetBag();
                GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().itemObject.Remove(go.gameObject);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.pickup_sound);
                break;
            case "Mouse":
                if (onX3Value) moneyAdd = 3 * go.GetComponent<Mouse>().GetValue();
                else if (go.GetComponent<Mouse>().isDiamond) moneyAdd = 2 + 800
                * camera.GetComponent<Player>().xValueDiamond;
                else moneyAdd = go.GetComponent<Mouse>().GetValue();
                camera.GetComponent<Player>().money += moneyAdd;
                GameObject.FindGameObjectWithTag("Play").GetComponent<UI>()
                    .OnMoneyAdd(moneyAdd);
                GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>()
                .itemObject.Remove(go.gameObject);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.pickup_sound);
                break;
        }
    }

    void RewindObject(GameObject go)
    {
        go.GetComponent<BoxCollider2D>().enabled = false;
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;

        go.transform.Translate(Vector3.down * 0.2f);
    }

    public void BreakGORewind()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().fireCreackerQuantity--;
        speedRewind = SPEED_REWIND_DEFAULT;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
        .PlayAudio(AudioMute.tnt_sound);
        StartCoroutine(AniExplodeFireCracker());
    }

    IEnumerator AniExplodeFireCracker()
    {
        GameObject explode = (GameObject)Instantiate(Resources.Load("Prefabs/ExplodeFireCracker"));
        explode.transform.position = goRewind.transform.position;
        Destroy(goRewind);
        yield return new WaitForSeconds(1f);
        Destroy(explode);
    }

    float GetSpeedRewind(float weight)
    {
        float normal = GameDefine.normalWeight;
        return SPEED_REWIND_DEFAULT / (normal * weight);
    }
}

