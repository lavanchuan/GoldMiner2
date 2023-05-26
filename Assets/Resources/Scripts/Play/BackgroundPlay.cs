using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlay : MonoBehaviour
{
    SpriteRenderer sr;
    Sprite sprite;
    int max = 3;
    int min = 1;
    public float currentTarget;
    public float target;
    int countICFireCracker = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        int k = Random.Range(min, max);
        sprite = Resources.Load<Sprite>("Graphics/Background/bg" + k);
        sr.sprite = sprite;
    }

    float GetTargetForLevel()
    {
        return (float)(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level + 1) / 50;
    }

    private void Start()
    {
        currentTarget = (0.5f + GetTargetForLevel()) * GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().GetTotal();
        target = currentTarget + GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().target;
        StartCoroutine(UpdateTime());

    }

    private void OnGUI()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        float money = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().money;
        // TARGET
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = screenWidth / 40;

        GUI.Label(new Rect(screenWidth / 10,
        0,
        screenWidth / 2 - screenWidth / 10,
        screenHeight / 10), "TARGET: $" + (int)target, style);

        // LEVEL
        GUI.Label(new Rect(screenWidth / 10,
        screenHeight / 10,
        screenWidth / 2 - screenWidth / 10,
        screenHeight / 10),
        "LEVEL: " + (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level + 1),
        style);

        // MONEY
        GUI.Label(new Rect(screenWidth / 2 + screenWidth / 10,
        0,
        screenWidth / 2 - screenWidth / 10,
        screenHeight / 10), "MONEY: $" + (int)money, style);

        // TIME
        GUI.Label(new Rect(screenWidth / 2 + screenWidth / 10,
        screenHeight / 10,
        screenWidth / 2 - screenWidth / 10,
        screenHeight / 10),
        "TIME(S): " + GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().GetTimeSecond(),
        style);

        // FIRECRACKER
        // if (countICFireCracker == 0)
        // {
        //     countICFireCracker++;
        //     GameObject icFireCracker = (GameObject)Instantiate(Resources.Load("Prefabs/FireCracker"));
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     Vector3 pos = player.transform.position;
        //     pos.x -= 1;
        //     icFireCracker.transform.position = pos;
        // }

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlay>().playerState == GameDefine.PlayerState.REWIND
            && GameObject.FindGameObjectWithTag("Hook").GetComponent<Hook>().goRewind != null
            && GameObject.FindGameObjectWithTag("Hook").GetComponent<Hook>().goRewind.tag != "Limit"
            && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().fireCreackerQuantity > 0)
        {
            GUIStyle s = new GUIStyle(GUI.skin.GetStyle("button"));
            int w = screenWidth / 15;
            int h = screenHeight / 10;
            int x = screenWidth / 2 - 2 * w;
            int y = h / 2;
            s.fontSize = screenHeight / 30;

            if (GUI.Button(new Rect(x, y, w, h), "BREAK", s))
            {
                GameObject.FindGameObjectWithTag("Hook").GetComponent<Hook>().BreakGORewind();
            }
        }



        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlay>().playState)
        {
            case GameDefine.PlayState.LOSE:
                LoseRender();
                break;
            default:
                break;
        }
    }

    void LoseRender()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        int w = 3 * screenWidth / 4;
        int h = screenHeight / 5;
        int x = (screenWidth - w) / 2;
        int y = screenHeight / 2;
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));
        style.fontSize = screenHeight / 20;

        string text = GameDefine.CAUSE_OF_LOSE;
        GUI.Label(new Rect(x, y - h, w, h), text, style);

        text = GameDefine.RESULT;
        GUI.Label(new Rect(x, y, w, h), text, style);

        if (GUI.Button(new Rect(x, y + h, w, h), "MENU", style))
        {
            StartCoroutine(AniTranslateMenu());
        }
    }

    IEnumerator UpdateTime()
    {
        while (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().GetTimeSecond() >= 0)
        {
            yield return new WaitForSeconds(1f);
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().playing)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().SetTimeSecond();
            }
        }
    }

    IEnumerator AniTranslateMenu()
    {
        yield return new WaitForSeconds(1f);

        GameObject.FindGameObjectWithTag("Play").GetComponent<CreateItemObject>().DestroyAll();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().Awake();

        Instantiate(Resources.Load("Prefabs/Menu"));
        Destroy(GameObject.FindGameObjectWithTag("Play"));
    }

}
