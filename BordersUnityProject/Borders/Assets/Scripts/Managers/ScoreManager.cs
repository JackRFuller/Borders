using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    //Managers
    private LevelManager lmScript;
    private UIManager uiScript;

    //Score Timer
    private int score = 0;
    private float timer = 1;

	// Use this for initialization
	void Start () {

        InitialiseData();
	
	}

    void InitialiseData()
    {
        lmScript = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        uiScript = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if(lmScript.currentGameState == LevelManager.gameState.InProgress)
        {
            AddOnScore();
        }
	
	}

    void AddOnScore()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            score++;
            timer = 1;
        }

        uiScript.ScoreUpdate(score);
    }

    public void AddOnPoints(int _pointsToAddOn)
    {
        score += _pointsToAddOn;

        uiScript.ScoreUpdate(score);
    }
}
