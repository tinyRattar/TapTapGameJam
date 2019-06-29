using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using DG.Tweening;

public class UIInGame :MonoSingleton<UIInGame> {

    public Transform stop;
    public Transform leftWin;
    public Transform rightWin;

    public void Start()
    {
        if (stop)
            stop.gameObject.SetActive(false);
        if (leftWin)
            leftWin.gameObject.SetActive(false);
        if (rightWin)
            rightWin.gameObject.SetActive(false);
    }

    public void Continue() {
        GameManager.Instance.Continue();
	}

    public void Quit () {
        Application.Quit();
	}

    public void ShowStop()
    {
        stop.gameObject.SetActive(true);
        //stop.DOLocalMoveY(-100, 1f);
    }

    public void Close()
    {
        //stop.DOLocalMoveY(100, 1f);
        stop.gameObject.SetActive(false);
    }

    public void ShowWin(bool isLeft)
    {
        StartCoroutine(InnerShowWin(isLeft));
    }

    public IEnumerator InnerShowWin(bool isLeft)
    {
        if (isLeft)
        {
            leftWin.gameObject.SetActive(true);
            yield return leftWin.DOLocalMoveY(0f, 0.2f);
        }
        else
        {
            yield return rightWin.DOLocalMoveY(0f, 0.2f);
            rightWin.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1);
    }
}
