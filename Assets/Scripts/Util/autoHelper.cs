using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoHelper : MonoBehaviour {

	[SerializeField]Vector3 backVec;
	[SerializeField]Vector3 frontVec;
	[SerializeField]float time = 1.0f;
	[SerializeField]bool show = false;
	[SerializeField]KeyCode keyShow = KeyCode.Tab;
	float timer=0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (show) {
			if (timer <= time) {
				timer += Time.deltaTime;
			}
		}else{
			if(timer >0){
				timer -= Time.deltaTime;
			}
		}
		this.transform.position = Vector3.Lerp (backVec, frontVec, timer / time);
		if (Input.GetKeyDown (keyShow))
			show = true;
		else if (Input.GetKeyUp (keyShow))
			show = false;
				
	}
}
