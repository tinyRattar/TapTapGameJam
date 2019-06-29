using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInStart : MonoBehaviour {

    public Transform[] howToPlay;
    private bool isLock = false;

    public void Start()
    {
        for (int i = 0; i < howToPlay.Length; i++)
        {
            howToPlay[i].gameObject.SetActive(false);
        }
    }

    public void GameStart()
    {
        if (!isLock)
            SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        if (!isLock)
            StartCoroutine(InnerHowToPlay());
    }

    public IEnumerator InnerHowToPlay()
    {
        isLock = true;
        float time = 0.5f;
        for (int i=0;i<howToPlay.Length;i++)
        {
            howToPlay[i].gameObject.SetActive(true);
            howToPlay[i].GetComponent<Image>().CrossFadeAlpha(0, 0.01f, false);
            howToPlay[i].GetComponent<Image>().CrossFadeAlpha(1, time, false);
            yield return new WaitForSeconds(time);
            yield return new WaitUntil(() => { return Input.anyKeyDown; });
            //howToPlay[i].GetComponent<Image>().CrossFadeAlpha(0, time, false);
            //yield return new WaitForSeconds(time);
            //howToPlay[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < howToPlay.Length; i++)
        {
            howToPlay[i].gameObject.SetActive(false);
        }
        isLock = false;
    }
}
