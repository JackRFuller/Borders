using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoreObjectSelectMenu : ScrollingMenu  {

	public GameObject[] coreShapes;
	public int coreShapeID;

	// Use this for initialization
	void Start () {

		coreShapeID = 0;

        InitialiseValues();
	
	}
	
	// Update is called once per frame
	void Update () {

        MoveItems();	
	}

	public void ShapeUpdate(int _objectID)
	{
		coreShapes[coreShapeID].SetActive(false);

		coreShapeID = _objectID;

		coreShapes[coreShapeID].SetActive(true);

		for(int i = 0; i < scrollingItems.Length; i++)
		{
			scrollingItems[i] = GetShape(i);
		}
	}

	GameObject GetShape(int _shapeID)
	{
		GameObject _shape = coreShapes[coreShapeID].transform.GetChild(_shapeID).gameObject;
		return _shape;
	}
}
