using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel : MonoBehaviour {

	public int winFlag = -1;//0 for left, 1 for right
	public int maxSlot = 5;
	public List<Element.ElementType> listRequire;
	public List<Element> listElementLeft = new List<Element>();
	public List<Element> listElementRight = new List<Element>();
    public bool isFinished = false;

	[SerializeField]Vector3 offsetX = new Vector3 (1.0f, 0, 0);
	[SerializeField]Vector3 offsetTeam = new Vector3 (6.0f, 0, 0);

	private List<List<Element>> listElement = new List<List<Element>>();

    public UIBigCampFire uiBigCampFire;


	public Vector3 getPosition(int team,int index){
		Vector3 result = this.transform.position;
		result = result + team * offsetTeam;
		result = result + team * index * offsetX + (1 - team) * (maxSlot - 1 - index) * offsetX;

		return result;
	}

	public int StateCheck(Element other,int team){
		Element target;
		if (listElement [1 - team].Count != 0) {
			target = listElement [1 - team] [listElement [1 - team].Count - 1];
//		foreach (Element target in listElement[1-team]) {
			if (Util.isCounter (other.type, target.type)) {
				return 1;
			}
		}
		if (listElement [team].Count != 0) {
			target = listElement [team] [listElement [team].Count - 1];
			if (Util.isCounter (other.type, target.type)) {
				return 1;
			}
			if (other.type == target.type) {
				return 2;
			}
		}

		return 0;
	}
	/// <summary>
	/// call when new element enter channel
	/// </summary>
	/// <param name="other">Other.</param>
	/// <param name="team">Team:0 for Left,1 for Right.</param>
	public bool OnElementEnter(Element other, int team){
		int index = -1;
		if (isFinished)
			return false;
		index = CheckEnemyKill (other, team);
		if (index >= 0) {
			other.MoveTo (getPosition (1 - team, index));
			listElement [1 - team].RemoveAt (index);
			for (int i = index; i < listElement [1 - team].Count; i++) {
				listElement [1- team] [i].MoveTo (getPosition (1 - team, i), delay: 1f);
			}
			CheckWin (1 - team);
			return true;
		}
		index = CheckSelfKill (other, team);
		if (index >= 0) {
			other.MoveTo (getPosition (team, index));
			//Debug.Log (index+""+listElement[team].Count);
			listElement [team].RemoveAt (index);
			for (int i = index; i < listElement [team].Count; i++) {
				listElement [team] [i].MoveTo (getPosition (team, i), delay: 1f);
			}
			//Debug.Log (listElement [1 - team].Count);
			CheckWin (team);
			return true;
		}
		if (CheckCombine (other, team)) {
			other.MoveTo (getPosition (team, listElement [team].Count-1));
			CheckWin (team);
			return true;
		}
		listElement [team].Add (other);
		other.MoveTo (getPosition (team, listElement [team].Count-1));
		if (CheckWin (team))
			return true;
		//check full
		if (listElement [team].Count >= maxSlot) {
			SetWinner (1 - team);
		}

		return true;
	}

	public void SetWinner(int team){
		for (int i = 0; i < listElement[team].Count; i++) {
			listElement [team] [i].OnFinish ();
		}
		isFinished = true;
		winFlag = team;
        uiBigCampFire.StartFire(winFlag);
        this.GetComponentInParent<Campfire> ().gainScore (team);
		foreach (autoFader fader in GetComponentsInChildren<autoFader>()) {
			fader.StartFade ();
		}
		//anim
	}

	public int CheckEnemyKill(Element other, int team){
		int index = 0;
		if (listElement [1 - team].Count != 0) {
			Element target = listElement [1 - team] [listElement [1 - team].Count - 1];
			//foreach (Element target in listElement[1-team]) {
			if (Util.isCounter (other.type, target.type)) {
				other.OnKill (true);
				target.OnKill (false);
				//Debug.Log (index);
				return listElement [1-team].Count - 1;
			}
			index++;
		}

		return -1;
	}

	public int CheckSelfKill(Element other, int team){
		int index = 0;
		if (listElement [team].Count == 0)
			return -1;
		Element target = listElement [team] [listElement[team].Count - 1];
		if (Util.isCounter (other.type, target.type)) {
			other.OnKill (true);
			target.OnKill (false);
			return listElement [team].Count - 1;
		}

		return -1;

		//return CheckEnemyKill (other, 1 - team);
	}

	public bool CheckCombine(Element other, int team){
		if (listElement [team].Count == 0)
			return false;
		Element target = listElement [team] [listElement[team].Count - 1];
		if (other.type == target.type) {
			other.OnCombine (false);
			target.OnCombine (true);
			return true;
		}

		return false;
	}

	private bool CheckWin(int team){
		//===unset highlight[maybe required]===
		foreach (Element target in listElement[team])
			target.SetHighlight (false);
		for (int i = 0; i < listRequire.Count; i++) {
			if (listElement [team].Count <= i)
				return false;
			if (listElement [team] [i].type != listRequire [i])
				return false;
			listElement [team] [i].SetHighlight (true);
		}
		Debug.Log ("setwinner?");
		SetWinner (team);

		return true;
	}

	// Use this for initialization
	void Start () {
		listElement.Add (listElementLeft);
		listElement.Add (listElementRight);
        for (int i = 0; i < Random.Range(GameManager.Instance.minElementInChannel, GameManager.Instance.maxElementInChannel + 1); i++)
        {
            listRequire.Add(Util.randomElementType());
            uiBigCampFire.SetDiamond(listRequire[i]);
        }
        uiBigCampFire.ShowDiamond();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
