using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private LevelManager lmScript;

    [Header("Core UI")]
    [SerializeField] private GameObject gameUI;

    [Header("Health")]
    [SerializeField] private Image healthBar;   
    
    [Header("Score")]
    [SerializeField] private Text scoreText;

    //Lerping Variables
    private float timeTakenDuringLerp = 1F;
    private float distanceToMove;
    private bool isLerping;

    private float startValue;
    private float endValue;

    private float timeStartedLerping;


    public void InitialiseData(LevelManager _lmScript)
    {
        lmScript = _lmScript;
    }
	
	// Update is called once per frame
	void Update () {

        if (isLerping)
        {
            HealthBarLerp();
        }
	
	}

    public void UpdateCoins(int NumOfCoins)
    {

    }

    public void LoseHealth(float _healthLost)
    {
        float _startingPosition = healthBar.fillAmount;
        float _endingPosition = healthBar.fillAmount - _healthLost;       

        StartLerping(_startingPosition, _endingPosition);
    }

    void StartLerping(float _startPos, float _endPos)
    {
        isLerping = true;
        timeStartedLerping = Time.time;

        startValue = _startPos;
        endValue = _endPos;
    }

    public void HealthBarLerp()
    {
        float _timeSinceStarted = Time.time - timeStartedLerping;
        float _percentageComplete = _timeSinceStarted / timeTakenDuringLerp;

        healthBar.fillAmount = Mathf.Lerp(startValue, endValue, _percentageComplete);

        if (healthBar.fillAmount <= 0)
        {
            lmScript.GameOver();
        }

        if (_percentageComplete >= 1.0F)
        {
            isLerping = false;
        }
    }

    public void ScoreUpdate(float _score)
    {
        scoreText.text = _score.ToString("F0");
    }

    public void TurnOnGameUI()
    {
        gameUI.SetActive(true);
    }
}
