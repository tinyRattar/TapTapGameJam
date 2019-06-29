using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBigCampFire : MonoBehaviour {

    public Transform redFire;
    public Transform blueFire;
    public Transform left;
    public Transform right;
    public Transform up;
    private List<Transform> diamond = new List<Transform>();
	// Use this for initialization
	void Start () {
        redFire.gameObject.SetActive(false);
        blueFire.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartFire(int team)
    {
        if (team == 0)
            redFire.gameObject.SetActive(true);
        else
            blueFire.gameObject.SetActive(true);
        HideDiamond();
		this.GetComponent<AudioSource> ().Play ();
    }

    public void SetDiamond(Element.ElementType type)
    {
        diamond.Add(Util.getDiamond(type, this.transform));
    }

    public void ShowDiamond()
    {
		Debug.Log ("work");
        Vector3 pivot = left.position;
        float width = (right.position.x - left.position.x) / (diamond.Count - 1);
        float height = (up.position.y - (right.position.y + left.position.y) / 2) / (diamond.Count - 1 );
        if (diamond.Count == 2)
        {
            diamond[0].position = new Vector3(pivot.x + width * 1 / 5, pivot.y,0);
            diamond[1].position = new Vector3(pivot.x + width * 4 / 5, pivot.y, 0);
        }
        else
        {
            diamond[0].position = new Vector3(pivot.x, pivot.y, 0);
            diamond[1].position = new Vector3(pivot.x + width, pivot.y + height, 0);
            diamond[2].position = new Vector3(pivot.x + width * 2, pivot.y, 0);
        }
    }

    public void HideDiamond()
    {
        for(int i=0;i<diamond.Count;i++)
        {
            diamond[i].gameObject.SetActive(false);
        }
    }
}
