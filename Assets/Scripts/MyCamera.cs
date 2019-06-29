using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyCamera : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Camera.main.orthographicSize = GetSize();
    }

    float GetSize()
    {
        float temp = (float)(Screen.width) / Screen.height;
        float temp2 = 0;
		if (temp < (float)4 / 3 + 0.05f) {
			temp2 = 7.2f;
		} else if (temp < 1.5f + 0.05f) {
			temp2 = 6.8f;
		} else if (temp < (float)16 / 10) {
			temp2 = 6.2f;
		} else if (temp < (float)16 / 9) {
			temp2 = 5.8f;
		} else {
			temp2 = 5.4f;
		}
        return temp2;
    }

	void Update(){
		Camera.main.orthographicSize = GetSize();
	}
}
