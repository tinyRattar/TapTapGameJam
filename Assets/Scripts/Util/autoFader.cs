using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class autoFader : MonoBehaviour {

	public bool onFade = false;
	public float time = 1.0f;
	float timer = 0.0f;
	[SerializeField]bool isImage = false;

	public void StartFade(float iTime = -1f){
		if (iTime > 0f)
			time = iTime;
		onFade = true;
		timer = 0.0f;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (onFade) {
			if (timer < time)
				timer += Time.deltaTime;
			if (isImage) {
				//this.GetComponent<Image> ().color = new Color (1, 1, 1, 1 - timer / time);
				this.GetComponent<Image> ().fillAmount = 1 - timer / time;
			}
			else
				this.GetComponent<SpriteRenderer> ().color = new Color (0.2f, 0.2f, 0.2f, timer / time * 0.5f);
		}

		
	}
}
