using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class androidInputTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount>0 ){
			foreach (Touch touch in Input.touches) {
				if(touch.phase ==TouchPhase.Began)
					Debug.Log (touch.position);
			}
		}
	}
}
