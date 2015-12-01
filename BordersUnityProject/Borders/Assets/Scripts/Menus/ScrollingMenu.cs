using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class ScrollingMenu : MonoBehaviour {

    public enum scrollType
    {
        Text,
        Image,
    }

    public scrollType currentScrollType;

	public RectTransform panel; //Holds the Scroll Panel
	public Text[] shapeText; //Holds the Shape names

    public Image[] imageObjects; //Holds the core objects

	public RectTransform center; //Center to compare the distance for each text
	public int objectID = 0;

	private float[] distance; //Holds distance between text and center
	private bool dragging = false; //Will be true, while the panel is dragged
	private int textDistance; //Holding the distance between the text
	private int minTextNum; //Hold the number of text, with the smallet distance to the center




	public void InitialiseValues()
	{
        int _textLength = 0;

        if(currentScrollType == scrollType.Text)
        {
            _textLength = shapeText.Length;
        }

        if(currentScrollType == scrollType.Image)
        {
            _textLength = imageObjects.Length;
        }
        
		distance = new float[_textLength];

        //Get distance between text

        if (currentScrollType == scrollType.Text)
        {
            textDistance = (int)Mathf.Abs(shapeText[1].GetComponent<RectTransform>().anchoredPosition.x - shapeText[0].GetComponent<RectTransform>().anchoredPosition.x);
        }


        if (currentScrollType == scrollType.Image)
        {
            textDistance = (int)Mathf.Abs(imageObjects[1].GetComponent<RectTransform>().anchoredPosition.x - imageObjects[0].GetComponent<RectTransform>().anchoredPosition.x);
        }

       
	}

	public void MoveItems()
	{
        if(currentScrollType == scrollType.Text)
        {
            MoveTextItems();
        }

        if(currentScrollType == scrollType.Image)
        {
            MoveImageItems();
        }

		
	}

    void MoveImageItems()
    {
        for (int i = 0; i < imageObjects.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - imageObjects[i].transform.position.x);
        }

        float _minDistance = Mathf.Min(distance); //Get the minimum distance

        for (int j = 0; j < imageObjects.Length; j++)
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

    void MoveTextItems()
    {
        for (int i = 0; i < shapeText.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - shapeText[i].transform.position.x);
        }

        float _minDistance = Mathf.Min(distance); //Get the minimum distance

        for (int j = 0; j < shapeText.Length; j++)
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
	}



}
