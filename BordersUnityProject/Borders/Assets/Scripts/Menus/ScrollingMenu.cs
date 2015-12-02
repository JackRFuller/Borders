using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class ScrollingMenu : MonoBehaviour {

	public RectTransform panel; //Holds the Scroll Panel

	public GameObject[] scrollingItems;

	public RectTransform center; //Center to compare the distance for each text
	public int objectID = 0;
	public int lastObjectChosen = 0;
	public bool objectChanged;

	private float[] distance; //Holds distance between text and center
	private bool dragging = false; //Will be true, while the panel is dragged
	private int textDistance; //Holding the distance between the text
	private int minTextNum; //Hold the number of text, with the smallet distance to the center




	public void InitialiseValues()
	{
        int _textLength = 0;
        
		_textLength = scrollingItems.Length;
        
		distance = new float[_textLength];

        //Get distance between text
		textDistance = (int)Mathf.Abs(scrollingItems[1].GetComponent<RectTransform>().anchoredPosition.x - scrollingItems[0].GetComponent<RectTransform>().anchoredPosition.x);
	}

	public void MoveItems()
	{
		for (int i = 0; i < scrollingItems.Length; i++)
		{
			distance[i] = Mathf.Abs(center.transform.position.x - scrollingItems[i].transform.position.x);
		}
		
		float _minDistance = Mathf.Min(distance); //Get the minimum distance
		
		for (int j = 0; j < scrollingItems.Length; j++)
		{
			if (_minDistance == distance[j])
			{
				minTextNum = j;
				
				//Determines what object has been chosen
				objectID = j;
			}
		}
		
		if (!dragging)
		{
			LerpToObject(minTextNum * -textDistance);			
		}
	}


	void LerpToObject(int _position)
	{
		float _newX = Mathf.Lerp(panel.anchoredPosition.x, _position, Time.deltaTime * 10F);
		Vector2 _newPos = new Vector2(_newX, panel.anchoredPosition.y);

		panel.anchoredPosition = _newPos;
	}

	public void StartDrag()
	{
		dragging = true;
	}

	public void EndDrag()
	{
		dragging = false;   

		if(lastObjectChosen != objectID)
		{
			objectChanged = true;
			lastObjectChosen = objectID;
		}

	}



}
