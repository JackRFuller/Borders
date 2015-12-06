using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private CoreShapeController cscScript;
    private bool shapeFound;

	// Use this for initialization
	void Start () {

        
	}
    
    public void FindCoreShape(GameObject coreShape)
    {
        cscScript = coreShape.GetComponent<CoreShapeController>();

        if(cscScript != null)
        {
            shapeFound = true;
        }
    }	

    public void StartRotation(string _direction)
    {
        if (shapeFound)
        {
            cscScript.rotDirection = _direction;
            cscScript.rotating = true;
        }
       
    }

    public void EndRotation()
    {
        if (shapeFound)
        {
            cscScript.rotating = false;
        }
        
    }
}
