using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScrollingBar : ScrollingMenu {

    [Header("Managers")]
    [SerializeField] private ResetManager rmScript;
    [SerializeField] private SpawnManager smScript;
    [SerializeField] private LevelSetupManager lsmScript;    

    [Header("End Game")]
    [SerializeField] private Animation gameOverPanel;

	// Use this for initialization
	void Start () {

        InitialiseValues();

        TurnOffInActiveButtons();
	
	}
	
	// Update is called once per frame
	void Update () {

        MoveItems();
	
	}

    public void TurnOffInActiveButtons()
    {
        for(int i = 0; i < scrollingItems.Length; i++)
        {
            if(i != objectID)
            {
                scrollingItems[i].GetComponent<Button>().enabled = false;
            }
            else
            {
                scrollingItems[i].GetComponent<Button>().enabled = true;
            }
        }
    }

    public void EndGame()
    {
        gameOverPanel.Play("GameOverOut");
        rmScript.ResetLevelData();
       
        lsmScript.LoadInLevelSelect();
    }

    public void Restart()
    {
        rmScript.ResetLevelData();
        gameOverPanel.Play("GameOverOut");

        rmScript.ResetShape();
    }    
}
