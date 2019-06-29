using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {
    SpriteRenderer r;
	// Use this for initialization
	void Start () {
        r = GetComponent<SpriteRenderer>();
        Material mat = (Material)ResourceManager.Load(Setting.Path.MAT_Dissolve);
        mat.SetTexture("_MainTex", r.sprite.texture);
        r.material = mat;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
