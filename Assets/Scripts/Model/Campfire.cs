using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Campfire : MonoBehaviour {

	public int goalScore = 3;
	public int[] score = new int[2];
    public bool isWin = false;
	[SerializeField]Team team1;
	[SerializeField]Team team2;

	public void gainScore(int team){
		score [team]++;
		if (score [team] >= goalScore) {
			gameOver (team);
		}
		if (score [0] + score [1] == goalScore * 2 - 2) {
			team1.cdTime = 0.2f;
			team2.cdTime = 0.2f;
			team1.freshCdTime = 1.0f;
			team2.freshCdTime = 1.0f;
		}
	}

	public void gameOver(int team)
    {
        UIInGame.Instance.ShowWin(team == 0);
        isWin = true;
        Debug.Log ("winner is team" + team + "!");
		//do something
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isWin && Input.anyKeyDown)
        {
            StartCoroutine(Restart());
        }
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
