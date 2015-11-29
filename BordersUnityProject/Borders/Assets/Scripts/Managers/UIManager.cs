using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private LevelManager lmScript;

    [Header("Health")]
    [SerializeField] private Image healthBar;   
    
    [Header("Score")]
    [SerializeField] private Text scoreText; 

	// Use this for initialization
	void Start () {

        InitialiseData();
	
	}

    void InitialiseData()
    {
        lmScript = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoseHealth(float _healthLost)
    {
        healthBar.fillAmount -= _healthLost;

        if(healthBar.fillAmount <= 0)
        {
            lmScript.GameOver();
        }
    }

    public void ScoreUpdate(int _score)
    {
        scoreText.text = _score.ToString();
    }
}
