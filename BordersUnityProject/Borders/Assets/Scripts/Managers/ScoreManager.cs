using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    //Managers
    private LevelManager lmScript;
    private UIManager uiScript;

    //Score Timer
    private float score = 0;
    private float timer = 1;

    private bool setupData;

    //Lerping Data
    private float timeTakenDuringLerp = 0.5F;
    private bool isLerping;
    private float startValue;
    private float endValue;

    private float timeStartedLerping;   

    public void InitialiseData(LevelManager _lmScript, UIManager _uiScript)
    {
        if(lmScript == null && uiScript == null)
        {
            lmScript = _lmScript;
            uiScript = _uiScript;

            setupData = true;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        if (isLerping)
        {
            LerpScore();
        }
	}

    public void AddOnPoints(int _pointsToAddOn)
    {
        isLerping = true;
        timeStartedLerping = Time.time;

        startValue = score;
        endValue = score + _pointsToAddOn;
    } 

    void LerpScore()
    {
        float _timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = _timeSinceStarted / timeTakenDuringLerp;

        score = Mathf.Lerp(startValue, endValue, percentageComplete);
        uiScript.ScoreUpdate(score);

        if(percentageComplete >= 1.0F)
        {
            isLerping = false;
        }
    }

    
}
