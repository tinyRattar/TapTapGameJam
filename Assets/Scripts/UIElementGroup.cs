using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.UI;
using DG.Tweening;

public class UIElementGroup :  AbstructUIGroup{

    public float time = 1f;
    public bool test = false;

    private List<Vector2> positions = new List<Vector2>();
    private const int MAIN_CARD = 0;
    private readonly Vector3 MAIN_SCALE = new Vector3(3,3,0);
    private readonly Vector3 ORIGINAL_SCALE = new Vector3(2, 2, 0);

    private void Start()
    {
        Init();
        //units.ForEach(c => pos.Add(c.transform.localPosition));
        foreach (var unit in units)
        {
            positions.Add(unit.transform.position);
        }
    }

    private void Update()
    {
        if (test)
        {
            Execute();
            test = false;
        }
    }

    public override void Execute()
    {
        for (int i=0;i<units.Count;i++)
        {
            units[i].transform.DOLocalMoveY(positions[i + 1 < positions.Count ? i + 1 : 0].y, time);
            if (i == MAIN_CARD)
                units[i].transform.DOScale(MAIN_SCALE, time);
            else
                units[i].transform.DOScale(ORIGINAL_SCALE, time);
        }
        for (int i = 0; i < units.Count - 1; i++)
        {
            AbstractUIUnit unit = units[0];
            units.Remove(unit);
            units.Add(unit);
        }
        for (int i = 0; i < units.Count; i++)
        {
            if (i == MAIN_CARD)
                units[i].GetComponent<SpriteRenderer>().sortingOrder = Setting.UISortingOrder.LAYER1;
            else
                units[i].GetComponent<SpriteRenderer>().sortingOrder = Setting.UISortingOrder.LAYER2;
        }
    }
}
