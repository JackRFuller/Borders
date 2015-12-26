using UnityEngine;
using System.Collections;

public class ComboManager : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private UIManager uiScript;

    public int comboMultiplier = 1;
    public int[] pelletHitsNeeded;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void IncreaseCombo()
    {
        float _fillAmount = 100 / pelletHitsNeeded[comboMultiplier - 1];
        _fillAmount /= 100;

        uiScript.IncreaseComboBar(_fillAmount);
    }

    public void ResetCombo()
    {
        comboMultiplier = 1;
        uiScript.ResetCombobar();
    }
}
