using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {
	
	enum State{
		none,
		kill,
		combine,
		highlight
	}

	public List<Sprite> listBorder;
	State state;

	float timer = 0f;
	float fadeTime = 0.5f;

	public void setState(int iState = 0){
		state = (State)iState;
		if (iState == 0) {
			//this.GetComponent<SpriteRenderer> ().sprite = null;
		} else {
			this.GetComponent<SpriteRenderer> ().sprite = listBorder [iState - 1];
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.none:
			if (timer >= 0f)
				timer -= Time.deltaTime;
			break;
		case State.combine:
			if (timer <= fadeTime)
				timer += Time.deltaTime;
			break;
		case State.kill:
			if (timer <= fadeTime)
				timer += Time.deltaTime;
			break;
		case  State.highlight:
			if (timer <= fadeTime)
				timer += Time.deltaTime;
			break;
		}
		this.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, timer / fadeTime);

	}
}
