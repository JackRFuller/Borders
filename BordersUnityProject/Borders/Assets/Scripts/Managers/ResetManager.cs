using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private ScoreManager smScript;
    [SerializeField] private UIManager uiScript;
    [SerializeField] private SpawnManager spmScript;
    [SerializeField] private LevelSetupManager lsmScript;

	// Use this for initialization
	void Start () {
	
	}	

    public void ResetLevelData()
    {
        smScript.ResetScore();
        uiScript.ResetHealthBar();
        uiScript.ReserGameOverPanel();
        spmScript.TurnOffAllPellets();
    }

    public void ResetShape()
    {
        StartCoroutine(lsmScript.GameLoadUpSequence());
    }


}
