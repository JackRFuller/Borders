using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private LevelManager lmScript;
    [SerializeField] private ComboManager cmScript;

    [Header("Core UI")]
    [SerializeField] private GameObject gameUI;   

    [Header("Health")]
    [SerializeField] private Image healthBar;  
    
    [Header("Combo")]
    [SerializeField] private Image comboBar; 
    [SerializeField] private Text comboText; 
    
    [Header("Score")]
    [SerializeField] private Text scoreText;

    [Header("Game Over Panel")]
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Animation gameOverPanel;
    public Transform[] gameOverPanels;
    private Vector3[] initialPanelPos = new Vector3[3];

    //Lerping Variables (Health Bar)
    private float timeTakenDuringLerp = 1F;
    private float distanceToMove;
    private bool isLerping;
    private float startValue;
    private float endValue;
    private float timeStartedLerping;

    //Lerping Variables (Combo Bar)
    private float timeTakenForLerp = 1F;
    private float movingDistance;
    private bool comboBarIsLerping;
    private float startPoint;
    private float endPoint;
    private float timeBeginningLerping;
    private bool addedCombo;

    void Start()
    {
        for(int i = 0; i < gameOverPanels.Length; i++)
        {
            initialPanelPos[i] = gameOverPanels[i].localPosition;
        }
    }

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

        if (comboBarIsLerping)
        {
            LerpComboBar();
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

    void LerpComboBar()
    {
        float _timeSinceStarted = Time.time - timeBeginningLerping;
        float _percentageComplete = _timeSinceStarted / timeTakenForLerp;
       
        comboBar.fillAmount = Mathf.Lerp(startPoint, endPoint, _percentageComplete);        
       
        if (comboBar.fillAmount == 1)
        {
            IncreaseCombo();
            comboBarIsLerping = false;
        }

        if (_percentageComplete >= 1.0F)
        {
            comboBarIsLerping = false;
        }
    }

    void IncreaseCombo()
    {       
        if (cmScript.comboMultiplier < cmScript.pelletHitsNeeded.Length)
        {
            cmScript.comboMultiplier++;
            Debug.Log(cmScript.comboMultiplier);
            comboText.text = "x" + (cmScript.comboMultiplier).ToString();
            comboBar.fillAmount = 0;
        }        
    }

    public void ScoreUpdate(float _score)
    {
        scoreText.text = _score.ToString("F0");
    }

    public void FinalScoreUpdate(float _score)
    {
        finalScoreText.text = _score.ToString("F0");
    }

    public void TurnOnGameUI()
    {
        gameUI.SetActive(true);        
    }

    public void TurnOffGameUI()
    {
        gameUI.SetActive(false);
        
    }
        
    public void ResetHealthBar()
    {
        healthBar.fillAmount = 1;
    }

    public void IncreaseComboBar(float _increasedFillAmount)
    {
        startPoint = comboBar.fillAmount;
        endPoint = comboBar.fillAmount += _increasedFillAmount;

        comboBarIsLerping = true;
        timeBeginningLerping = Time.time;
    }

    public void ResetCombobar()
    {
        comboBar.fillAmount = 0;
        comboBarIsLerping = false;
        comboText.text = "";
    }

    public void ResetGameOverPanel()
    {
        for (int i = 0; i < gameOverPanels.Length; i++)
        {
             gameOverPanels[i].localPosition = initialPanelPos[i];
        }
    }
}
