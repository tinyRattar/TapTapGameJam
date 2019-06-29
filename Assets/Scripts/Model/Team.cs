using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.UI;
using DG.Tweening;
using UnityEngine.UI;

public class Team : MonoBehaviour
{
	[SerializeField]KeyCode keyUp = KeyCode.W;
	[SerializeField]KeyCode keyDown = KeyCode.S;
	[SerializeField]KeyCode keySwitch = KeyCode.A;
	[SerializeField]KeyCode keyFire = KeyCode.D;
	[SerializeField]KeyCode keyFresh = KeyCode.Q;
	[SerializeField]Campfire campfire;
	[SerializeField]GameObject cdTimerHelper;
	[SerializeField]GameObject cdFreshHelper;

	public List<Element> handCard;
	public List<Channel> listChannel;
	public bool ctrlAble = true;
	int index = 0;
	int indexCh = 0;
	[SerializeField]int team = 0;
	public float cdTime = 1f;
	public float freshCdTime = 5f;
	float timer = 0f;
	float freshTimer = 0f;

    public Vector3[] positions = new Vector3[3];

    public float time = 1f;
	public float offsetY = 3f;
    public bool test = false;

	public GameObject channelSelector;
	public List<float> offsetCh;

    private static readonly Vector3 ORIGINAL_SCALE = new Vector3(0.1f, 0.1f, 0);
    private static readonly Vector3 MAIN_SCALE = ORIGINAL_SCALE * 1.5f;

    public bool OnCast(){
		GameObject newElement;
		if (listChannel [indexCh].isFinished)
			return false;
		newElement = GameObject.Instantiate (handCard [index].gameObject);
		newElement.transform.position = handCard [index].transform.position;
		if (listChannel [indexCh].OnElementEnter (newElement.GetComponent<Element>(), team))
        {
			newElement.transform.localScale = ORIGINAL_SCALE;
            handCard [index].type = Util.randomElementType ();
			handCard [index].UpdateSprite ();
			timer = cdTime;
			PlayAudio ();
        } else {
			Debug.Log ("no!!!");
			Destroy (newElement);
		}

		return true;
	}
		
	public void OnFresh(){
		for (int i = 0; i < handCard.Count; i++) {
			handCard [i].type = Util.randomElementType ();
			handCard [i].UpdateSprite ();
		}
		freshTimer = freshCdTime;
		PlayAudio ();
	}

	public void PlayAudio(){
		this.GetComponent<AudioSource> ().Play ();
	}

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < GameManager.Instance.maxHandCard; i++)
        {
            Element e = Util.randomElement(transform);
			e.transform.localPosition = new Vector3 (0, -offsetY, 0) * i;
            handCard.Add(e);
        }
        handCard[index].transform.localScale = MAIN_SCALE;
    }

	public void dealFire(){
		if (timer <= 0) {
			if (OnCast ()) {
				dealSwitch ();
				cdTimerHelper.GetComponent<Image> ().fillAmount = timer / cdTime;
				cdTimerHelper.GetComponent<autoFader> ().StartFade (cdTime);
			}

		} 


	}

	public void dealSwitch(){
		index = (index + 1) % handCard.Count;
		Execute ();
	}

	public void dealFresh(){
		if (freshTimer <= 0) {
			OnFresh ();
			cdFreshHelper.GetComponent<Image> ().fillAmount = freshTimer / freshCdTime;
			cdFreshHelper.GetComponent<autoFader> ().StartFade (freshCdTime);
		} 
	}

	public void dealSelectCh(int i){
		if (!campfire.isWin && !listChannel [i].isFinished) {
			indexCh = i;
			Vector3 pos = channelSelector.transform.localPosition;
			pos.y = offsetCh [indexCh];
			channelSelector.transform.localPosition = pos;
		}
	}

	// Update is called once per frame
	void Update () 
	{		
		if (ctrlAble) {
			if (Input.GetKeyDown (keyUp)) {
				if (!campfire.isWin) {
					indexCh += listChannel.Count - 1;
					indexCh = indexCh % listChannel.Count;
					while (listChannel [indexCh].isFinished) {
						indexCh += listChannel.Count - 1;
						indexCh = indexCh % listChannel.Count;
					}
					Vector3 pos = channelSelector.transform.localPosition;
					pos.y = offsetCh [indexCh];
					channelSelector.transform.localPosition = pos;

				}
			} else if (Input.GetKeyDown (keyDown)) {
				if (!campfire.isWin) {
					indexCh += listChannel.Count + 1;
					indexCh = indexCh % listChannel.Count;
					while (listChannel [indexCh].isFinished) {
						indexCh += listChannel.Count + 1;
						indexCh = indexCh % listChannel.Count;
					}
					Vector3 pos = channelSelector.transform.localPosition;
					pos.y = offsetCh [indexCh];
					channelSelector.transform.localPosition = pos;
				}
			}

			if (Input.GetKeyDown (keySwitch)) {
				index = (index + 1) % handCard.Count;
				Execute ();
			}
			if (Input.GetKeyDown (keyFire)) {
				//Debug.Log ("fire" + index);
				if (timer <= 0) {
					OnCast ();
				} else {
					cdTimerHelper.GetComponent<Image> ().fillAmount = timer / cdTime;
					cdTimerHelper.GetComponent<autoFader> ().StartFade ();
				}
			}if (Input.GetKeyDown (keyFresh)) {
				if (freshTimer <= 0) {
					OnFresh ();
				} else {
					cdTimerHelper.GetComponent<Image> ().fillAmount = freshTimer / freshCdTime;
					cdTimerHelper.GetComponent<autoFader> ().StartFade ();
				}
			}
		}
		for (int i = 0; i < handCard.Count; i++) {
			if (i == index)
				handCard [i].GetComponentInChildren<Border> ().setState (listChannel [indexCh].StateCheck (handCard [index], team));
			else
				handCard [i].GetComponentInChildren<Border> ().setState (0);
		}
		if (timer > 0)
			timer -= Time.deltaTime;
		if (freshTimer > 0)
			freshTimer -= Time.deltaTime;
        if (test)
        {
            Execute();
            test = false;
        }
    }
    public void Execute()
    {
        for (int i = 0; i < handCard.Count; i++)
        {
			if (i == index - 2 || i == index + 1) {
				handCard [i].transform.DOLocalMoveY (-offsetY, 1f);
				handCard [i].GetComponent<SpriteRenderer> ().sortingOrder = Setting.UISortingOrder.LAYER1;
			} else if (i == index - 1 || i == index + 2) {
				handCard [i].transform.DOLocalMoveY (-2*offsetY, 1f);
				handCard [i].GetComponent<SpriteRenderer> ().sortingOrder = Setting.UISortingOrder.LAYER0;
			} else {
				handCard [i].transform.DOLocalMoveY (0, 1f);
				handCard [i].GetComponent<SpriteRenderer> ().sortingOrder = Setting.UISortingOrder.LAYER2;
			}
			if (i == index)
                handCard[i].transform.DOScale(MAIN_SCALE, time);
            else
                handCard[i].transform.DOScale(ORIGINAL_SCALE, time);
        }
    }
}
