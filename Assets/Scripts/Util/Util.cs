using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {

	static public bool isCounter(Element.ElementType src, Element.ElementType tar){
		int srcStandIn, tarStandIn;
		srcStandIn = (int)src;
		tarStandIn = (int)tar;
		//Debug.Log (src +":"+ srcStandIn + " ||" + tar +":"+ tarStandIn);
		srcStandIn = (srcStandIn + 1) % 5;
		if (srcStandIn == tarStandIn)
			return true;
		return false;
	}

	static public Element.ElementType getGenerateType(Element.ElementType src){
		switch (src) {
		case Element.ElementType.metal:
			return Element.ElementType.water;
		case Element.ElementType.water:
			return Element.ElementType.wood;
		case Element.ElementType.wood:
			return Element.ElementType.fire;
		case Element.ElementType.fire:
			return Element.ElementType.earth;
		case Element.ElementType.earth:
			return Element.ElementType.metal;
		default:
			break;
		}
		Debug.Log ("Error!");

		return Element.ElementType.earth;
	}

    public static Element randomElement(Transform parent)
    {
        GameObject go = ResourceManager.LoadGO(Setting.Path.PR_Element, parent);
        Element.ElementType et = randomElementType();
        go.GetComponent<SpriteRenderer>().sprite = getElementSprite(et);
        Element e = go.GetComponent<Element>();
        e.type = et;
        return e;
    }

    public static Transform getDiamond(Element.ElementType type, Transform parent)
    {
        GameObject go = ResourceManager.LoadGO(Setting.Path.PR_Diamond, parent);
        go.GetComponent<SpriteRenderer>().sprite = getDiamondSprite(type);
        return go.transform;
    }


    public static Sprite getElementSprite(Element.ElementType type)
    {
        string spPath = "";
        switch (type)
        {
            case Element.ElementType.metal:
                spPath = Setting.Path.SP_ElementMetal;
                break;
            case Element.ElementType.water:
                spPath = Setting.Path.SP_ElementWater;
                break;
            case Element.ElementType.wood:
                spPath = Setting.Path.SP_ElementWood;
                break;
            case Element.ElementType.fire:
                spPath = Setting.Path.SP_ElementFire;
                break;
            case Element.ElementType.earth:
                spPath = Setting.Path.SP_ElementEarth;
                break;
            default:
                break;
        }
        return ResourceManager.LoadTexture(spPath);
    }

    public static Sprite getDiamondSprite(Element.ElementType type)
    {
        string spPath = "";
        switch (type)
        {
            case Element.ElementType.metal:
                spPath = Setting.Path.SP_DMetal;
                break;
            case Element.ElementType.water:
                spPath = Setting.Path.SP_DWater;
                break;
            case Element.ElementType.wood:
                spPath = Setting.Path.SP_DWood;
                break;
            case Element.ElementType.fire:
                spPath = Setting.Path.SP_DFire;
                break;
            case Element.ElementType.earth:
                spPath = Setting.Path.SP_DEarth;
                break;
            default:
                break;
        }
        return ResourceManager.LoadTexture(spPath);
    }

    public static void dissolve(GameObject go)
    {
        Renderer r = go.GetComponent<Renderer>();
        r.material.shader = Shader.Find("Dissolve"); ;
    }

    public static Element.ElementType randomElementType()
    {
        return (Element.ElementType)Random.Range(0, 5);
    }
}
