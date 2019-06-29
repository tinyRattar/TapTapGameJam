using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
public class GameManager : MonoSingleton<GameManager> {
    public int minElementInChannel;
    public int maxElementInChannel;
    public int maxHandCard;
    Team[] teams;
    bool isStop = false;

    public bool test;
    public void Start()
    {
        teams = FindObjectsOfType<Team>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isStop)
            {
                Continue();
            }
            else
            {
                Stop();
            }
        }

        if (test)
            UIInGame.Instance.ShowWin(true);
    }

    public void Stop()
    {
        isStop = true;
        Time.timeScale = 0f;
        foreach(var team in teams)
        {
            team.ctrlAble = false;
        }
        UIInGame.Instance.ShowStop();
    }

    public void Continue()
    {
        isStop = false;
        Time.timeScale = 1f;
        foreach (var team in teams)
        {
            team.ctrlAble = true;
        }
        UIInGame.Instance.Close();
    }
}
