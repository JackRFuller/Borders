using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private ScoreManager smScript;
    [SerializeField] private UIManager uiScript;

    public enum gameState
    {
        Started,
        InProgress,
        GameOver,
    }

    public gameState currentGameState;

    [Header("Game Over Process")]
    [SerializeField] private Animation GameOverPanel;

    //LerpingVariables
    private float timeTakenToLerp = 2.5F;
    private bool isLerping;
    private float startPosition;
    private float endPosition;
    private float timeStartedLerping;
    private float finalScore;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isLerping)
        {
            LerpScore();
        }
	
    }  

    public void StartLevel()
    {
        currentGameState = gameState.InProgress;
    }

    public void GameOver()
    {
        if(currentGameState != gameState.GameOver)
        {
            currentGameState = gameState.GameOver;

            uiScript.TurnOffGameUI();

            BringInGameOverPanel();
        }
       
    }

    void BringInGameOverPanel()
    {
        
        GameOverPanel.Play("GameOverIn");

        SetupLerpingScore();
    }

    void SetupLerpingScore()
    {
        timeStartedLerping = Time.time;

        startPosition = 0;
        endPosition = smScript.score;

        isLerping = true;
    }

    void LerpScore()
    {
        float _timeSinceStarted = Time.time - timeStartedLerping;
        float _percentagecomplete = _timeSinceStarted / timeTakenToLerp;

        finalScore = Mathf.Lerp(startPosition, endPosition, _percentagecomplete);
        uiScript.FinalScoreUpdate(finalScore);

        if(_percentagecomplete >= 1.0F)
        {
            isLerping = false;
            BringInSaveMeButtons();
        }
    }

    void BringInSaveMeButtons()
    {
        GameOverPanel.Play("SaveMeIn");
    }

    public void BringInScoreBoard()
    {
        GameOverPanel.Play("ScoreBoardIn");
    }
}
