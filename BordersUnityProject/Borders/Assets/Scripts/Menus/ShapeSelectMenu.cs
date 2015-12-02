using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShapeSelectMenu : ScrollingMenu {

	private CoreObjectSelectMenu cobsmScript;

	[System.Serializable]
	public class CoreObjects
	{
		public GameObject[] coreShapes;
	}

	[Header("Core Objects")]
	public CoreObjects[] coreObjects;

	// Use this for initialization
	void Start () {

		cobsmScript = GameObject.Find("Core Object Select Manager").GetComponent<CoreObjectSelectMenu>();

		InitialiseValues();
	
	}
	
	// Update is called once per frame
	void Update () {

		MoveItems();

		if(objectChanged)
		{
			ChangeItems();
		}
	
	}

	void ChangeItems()
	{
		cobsmScript.ShapeUpdate(objectID);

		objectChanged = false;
	}
}
