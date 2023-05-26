using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlay : MonoBehaviour
{
    public GameDefine.PlayerState playerState;
    public GameDefine.PlayState playState;
    public bool uplevel = true;
    bool losed = false;
    bool wined = false;

    private void Awake()
    {
        playState = GameDefine.PlayState.PLAY;
        playerState = GameDefine.PlayerState.ROTATION;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("BtnPause").GetComponent<BtnPause>().isPause)
        {
            playState = GameDefine.PlayState.PAUSE;
        }
        else
        {
            playState = GameDefine.PlayState.PLAY;
        }

        switch (playState)
        {
            case GameDefine.PlayState.PLAY:
                PlayState();
                break;
            case GameDefine.PlayState.PAUSE:
                PauseState();
                break;

        }
    }

    int target;
    void PlayState()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().GetTimeSecond() < 0
                || GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().itemObject.Count == 0)
        {
            // [upleve]
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().playing = false;
            // check money to up level or lose
            target = (int)GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundPlay>().target;
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().money < target)
            {
                uplevel = false;
            }

            if (uplevel)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.win_sound);

                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().target = target;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level++;

                ArrayList items = GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().itemObject;
                foreach (GameObject go in items)
                {
                    Destroy(go);
                }
                
                if (!wined)
                {
                    wined = true;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                    .PlayAudio(AudioMute.win_sound);
                }
                GameObject menu = (GameObject)Instantiate(Resources.Load("Prefabs/Menu"));
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().gameScreen
                = GameDefine.GameScreen.MENU;
                Destroy(GameObject.FindGameObjectWithTag("Play"));
            }
            else
            {
                playState = GameDefine.PlayState.LOSE;
                if (!losed)
                {
                    losed = true;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                    .PlayAudio(AudioMute.lose_sound);
                }
            }

            // update player
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().strength = Player.STRENGTH_DEFAULT;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().xValueDiamond = Player.X_VALUE_DIAMOND_DEFAULT;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().onX3Value = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().timeAdd = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().useFourLeafClover = false;

        }
    }

    void PauseState()
    {

    }

}
