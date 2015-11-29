using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private CoreShapeController cscScript;

	// Use this for initialization
	void Start () {

        cscScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreShapeController>();	
	}	

    public void StartRotation(string _direction)
    {
        cscScript.rotDirection = _direction;
        cscScript.rotating = true;
    }

    public void EndRotation()
    {
        cscScript.rotating = false;
    }
}
