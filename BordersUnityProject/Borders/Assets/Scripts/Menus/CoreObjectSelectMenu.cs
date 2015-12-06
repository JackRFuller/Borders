using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoreObjectSelectMenu : ScrollingMenu  {

    private LevelSetupManager lsmScript;

    private int shapeCategory;
    private int coreObject;

	public GameObject[] coreShapes;
	public int coreShapeID;
    private bool setNewObject;

	// Use this for initialization
	void Start () {

        lsmScript = GameObject.Find("LevelSetupManager").GetComponent<LevelSetupManager>();
		coreShapeID = 0;
        InitialiseValues();
	
	}
	
	// Update is called once per frame
	void Update () {

        MoveItems();

        if (objectChanged)
        {
            coreObject = objectID;

            SetNewObject();

            objectChanged = false;
        }      
	}

    void SetNewObject()
    {
        lsmScript.SetCoreObject(shapeCategory, objectID);       
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

        if(shapeCategory != coreShapeID)
        {
            shapeCategory = coreShapeID;

            SetNewObject();
        }
    }

	GameObject GetShape(int _shapeID)
	{
		GameObject _shape = coreShapes[coreShapeID].transform.GetChild(_shapeID).gameObject;
		return _shape;
	}
}
