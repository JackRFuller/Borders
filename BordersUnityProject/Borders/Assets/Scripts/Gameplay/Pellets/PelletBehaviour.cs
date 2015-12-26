using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PelletBehaviour : CorePelletClass {  

	// Use this for initialization
	void Start () {

        InitialiseManagersAndCoreData();       
	}   
	
	// Update is called once per frame
	void Update () {

        MoveTowardsTarget();

        DetectShape();

        if(currentPelletState != pelletState.inFlight)
        {
            DetermineDeathSequence();
        }

    }
    
    void DetermineDeathSequence()
    {
        switch (currentPelletState)
        {
            case (pelletState.hitCorrectColour):
                CorrectColour();
                break;
            case (pelletState.hitWrongColour):
                IncorrectColour();
                break;
        }
    }
    
    void CorrectColour()
    {
        CoreShapeController cscScript = target.GetComponent<CoreShapeController>();
        cscScript.ShrinkShape();
        DetermineAddedScore();

        if (!addedCombo)
        {
            cmScript.IncreaseCombo();
            addedCombo = true;
        }
        correctColor = true;

        StartCoroutine(DeathSequence());
    }

    void IncorrectColour()
    {
        float _healthToLose = 0;
        _healthToLose = transform.localScale.x / 2;
        uiScript.LoseHealth(_healthToLose);
        correctColor = false;

        cmScript.ResetCombo();        

        StartCoroutine(DeathSequence());
    }    
}
