using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.UI;


public class Element : MonoBehaviour
{

	public enum ElementType{
		metal,
		wood,
		earth,
		water,
		fire
	}

	enum AfterFly{
		idle,
		combine,
		explo
	}
		
	public ElementType type = ElementType.metal;
	public float flyTime = 1.0f;
	public float fadeTime = 1.0f;
	public float afterFlyTime = 1.0f;
	public bool onFly = false;
	public bool onFade = false;
	public bool onAfterFly = false;
	bool onFinish = false;
	[SerializeField]GameObject finishCircle;
	[SerializeField]GameObject killCircle;
	AfterFly afterFly = AfterFly.idle;
	Vector3 tarPos = new Vector3 (0, 0, 0);
	Vector3 srcPos = new Vector3 (0, 0, 0);

	float timer = 0f;
	[SerializeField]float fadeTimer = 0f;
	float afterFlyTimer = 0f;
	float destoryTime = 2f;

	float fakeMaskScale = 1f;

	public bool OnKill(bool isMain = false){
		if (isMain) {
			
			afterFly = AfterFly.explo;
			//anim(attacker)
		} else {
			//MoveTo (Vector3.zero, 1f, true);
			afterFlyTimer = -1f;
			afterFly = AfterFly.explo;
			killCircle.transform.localPosition = Vector3.zero;
			GameObject.Instantiate (killCircle,this.transform);
			//anim
		}
		//Debug.Log ("onkill");
		Destroy (this.gameObject, destoryTime);

		return true;
	}

	public bool OnCombine(bool isMain){
		if (isMain) {
			type = Util.getGenerateType (type); 
			GameObject fakeMask = GameObject.Instantiate (this.gameObject, this.transform.position,this.transform.rotation,this.transform);
			fakeMask.GetComponent<Element> ().onFly = false;
			fakeMask.transform.localPosition = Vector3.zero;
			fakeMask.transform.localScale = Vector3.one;
			fakeMask.GetComponent<Element>().fakeMaskScale = 10f;
			fakeMask.GetComponent<SpriteRenderer> ().sortingOrder = 5;
			fakeMask.GetComponent<Element> ().FadeOut (delay:1f);
			Destroy (fakeMask, destoryTime);
			GetComponent<SpriteRenderer>().sprite = Util.getElementSprite(type);
			//anim
		} else {
			afterFly = AfterFly.combine;
			Destroy (this.gameObject, destoryTime);
			//anim
		}
		//Debug.Log ("onCombine "+isMain);

		return true;
	}

	public bool OnFinish(){
		Destroy (this.gameObject, destoryTime);
		FadeOut (delay:1f);
		onFinish = true;
		//anim
		Debug.Log ("onFinish");
		return true;
	}

	public void SetHighlight(bool flag){
		if (flag) {
			this.GetComponentInChildren<Border> ().setState (3);
		} else {
			this.GetComponentInChildren<Border> ().setState (0);
		}
	}

	public void UpdateSprite(){
		GetComponent<SpriteRenderer>().sprite = Util.getElementSprite(type);
	}

	public void MoveTo(Vector3 target, float time = 1.0f, bool asDelta = false,float delay = 0.0f){
		onAfterFly = false;
		this.GetComponent<SpriteRenderer> ().sortingOrder = 3;
		srcPos = this.transform.position;
		if (asDelta) {
			tarPos = srcPos + target;
		} else {
			tarPos = target;
		}
		flyTime = time;
		if(!onFly||timer<=0)
			timer = -delay;
		onFly = true;
	}

	public void FadeOut(float time = 1f,float delay = 0.0f){
		fadeTime = time;
		fadeTimer = -delay;
		onFade = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (onFly) {
			if (timer < flyTime)
				timer += Time.deltaTime;
			else {
				this.GetComponent<SpriteRenderer> ().sortingOrder = 2;
				timer = flyTime;
				onFly = false;
				onAfterFly = true;
			}
			if (timer >= 0) {
				this.transform.position = Vector3.Lerp (srcPos, tarPos, timer / flyTime);
			}
		}
		if (onFade) {
			if (fadeTimer < fadeTime)
				fadeTimer += Time.deltaTime;
			else {
				fadeTimer = fadeTime;
				onFade = false;
			}
			if (fadeTimer >= 0) {
				if (onFinish) {
					finishCircle.transform.position = this.transform.position;
					GameObject.Instantiate (finishCircle);
					onFinish = false;
				}
				this.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1 - fadeTimer / fadeTime);
				this.transform.Rotate (new Vector3 (0, 0, 15) * Time.deltaTime);
				this.transform.localScale = Vector3.Lerp (new Vector3 (0.1f * fakeMaskScale, 0.1f * fakeMaskScale, 1), new Vector3 (0.03f * fakeMaskScale, 0.03f * fakeMaskScale, 1), fadeTimer / fadeTime);
			}
		}if (onAfterFly) {
			if (afterFlyTimer < afterFlyTime)
				afterFlyTimer += Time.deltaTime;
			else {
				afterFlyTimer = afterFlyTime;
				//onFade = false;
			}
			if (afterFlyTimer >= 0) {
				switch (afterFly) {
				case AfterFly.explo:
					this.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1 - afterFlyTimer / afterFlyTime);
					this.transform.localScale = (Vector3.one + new Vector3 (1, 1, 0) * afterFlyTimer / afterFlyTime * 0.5f) * 0.1f;
					break;
				case AfterFly.combine:
					this.FadeOut ();
					afterFly = AfterFly.idle;
					break;
				default:
					break;
				}
			}

				
		}
		
		
	}
}
