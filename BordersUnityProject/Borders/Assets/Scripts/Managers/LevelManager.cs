using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public enum gameState
    {
        Started,
        InProgress,
        GameOver,
    }

    public gameState currentGameState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameOver()
    {
        currentGameState = gameState.GameOver;
    }
}
